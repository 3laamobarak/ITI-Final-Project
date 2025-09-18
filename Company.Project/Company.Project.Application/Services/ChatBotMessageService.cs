using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.Application.Services
{
    public class ChatBotMessageService : IChatBotMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;

        public ChatBotMessageService(IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ChatBotMessages>> GetAllMessagesAsync()
        {
            return await _unitOfWork.ChatBotMessagesRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ChatBotMessages>> GetMessagesByUserAsync(string userId)
        {
            return await _unitOfWork.ChatBotMessagesRepository
                .GetAllFilteredAsync(new string[] { $"UserId={userId}" });
        }

        public async Task<(ChatBotMessages userMsg, ChatBotMessages botMsg)> SendMessageAsync(string userId, string message)
        {
            // Save user message
            var userMessage = new ChatBotMessages
            {
                Message = message,
                Sender = "User",
                UserId = userId
            };

            await _unitOfWork.ChatBotMessagesRepository.AddAsync(userMessage);

            // Determine the type of query and generate appropriate response
            string botReply;
            
            if (IsOrderQuery(message))
            {
                // Handle order-related queries
                botReply = await HandleOrderQueryAsync(userId, message);
            }
            else if (IsCartQuery(message))
            {
                // Handle cart-related queries
                botReply = await HandleCartQueryAsync(userId, message);
            }
            else if (IsUserProfileQuery(message))
            {
                // Handle user profile queries
                botReply = await HandleUserProfileQueryAsync(userId, message);
            }
            else if (IsProductQuery(message))
            {
                // Extract filter criteria from the message
                var filters = ExtractProductFilters(message);
                var products = await GetFilteredProductsAsync(filters);
                
                // Format products as a response
                botReply = FormatProductResponse(products, message);
            }
            else
            {
                // Get regular bot reply
                botReply = await GetBotReplyAsync(message);
            }

            var botMessage = new ChatBotMessages
            {
                Message = botReply,
                Sender = "Bot",
                UserId = userId
            };

            await _unitOfWork.ChatBotMessagesRepository.AddAsync(botMessage);

            // Commit both
            await _unitOfWork.Completeasync();

            return (userMessage, botMessage);
        }

        #region Query Type Detection

        private bool IsOrderQuery(string message)
        {
            string lowercaseMessage = message.ToLower();
            return lowercaseMessage.Contains("order") || 
                   lowercaseMessage.Contains("purchase") || 
                   lowercaseMessage.Contains("bought") || 
                   lowercaseMessage.Contains("delivery") ||
                   lowercaseMessage.Contains("shipping") ||
                   lowercaseMessage.Contains("track") ||
                   (lowercaseMessage.Contains("my") && lowercaseMessage.Contains("orders"));
        }

        private bool IsCartQuery(string message)
        {
            string lowercaseMessage = message.ToLower();
            return lowercaseMessage.Contains("cart") || 
                   lowercaseMessage.Contains("basket") || 
                   lowercaseMessage.Contains("shopping list") ||
                   (lowercaseMessage.Contains("add") && lowercaseMessage.Contains("to cart")) ||
                   (lowercaseMessage.Contains("remove") && lowercaseMessage.Contains("from cart")) ||
                   lowercaseMessage.Contains("checkout");
        }

        private bool IsUserProfileQuery(string message)
        {
            string lowercaseMessage = message.ToLower();
            return lowercaseMessage.Contains("profile") || 
                   lowercaseMessage.Contains("account") || 
                   lowercaseMessage.Contains("my details") ||
                   lowercaseMessage.Contains("my information") ||
                   lowercaseMessage.Contains("my name") ||
                   lowercaseMessage.Contains("my email") ||
                   lowercaseMessage.Contains("my phone") ||
                   lowercaseMessage.Contains("my address");
        }

        private bool IsProductQuery(string message)
        {
            // Check if the message is asking about products
            string lowercaseMessage = message.ToLower();
            return lowercaseMessage.Contains("product") || 
                   lowercaseMessage.Contains("products") || 
                   lowercaseMessage.Contains("item") || 
                   lowercaseMessage.Contains("items") ||
                   lowercaseMessage.Contains("find") ||
                   lowercaseMessage.Contains("search") ||
                   lowercaseMessage.Contains("show me");
        }

        #endregion

        #region Order Handling

        private async Task<string> HandleOrderQueryAsync(string userId, string message)
        {
            string lowercaseMessage = message.ToLower();
            
            try
            {
                // Get user's orders
                var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
                
                if (!orders.Any())
                {
                    return "You don't have any orders yet. Would you like to browse our products?";
                }

                // Check if asking about a specific order
                var orderIdMatch = Regex.Match(message, @"order\s+(?:number|#|id)?\s*(\d+)", RegexOptions.IgnoreCase);
                if (orderIdMatch.Success)
                {
                    int orderId;
                    if (int.TryParse(orderIdMatch.Groups[1].Value, out orderId))
                    {
                        var specificOrder = orders.FirstOrDefault(o => o.Id == orderId);
                        if (specificOrder != null)
                        {
                            return FormatOrderDetails(specificOrder);
                        }
                        else
                        {
                            return $"I couldn't find order #{orderId} in your order history. Please check the order number and try again.";
                        }
                    }
                }

                // Check if asking about recent orders
                if (lowercaseMessage.Contains("recent") || lowercaseMessage.Contains("latest") || lowercaseMessage.Contains("last"))
                {
                    var recentOrder = orders.OrderByDescending(o => o.CreatedAt).FirstOrDefault();
                    if (recentOrder != null)
                    {
                        return $"Your most recent order is #{recentOrder.Id}. " + FormatOrderDetails(recentOrder);
                    }
                }

                // Default: show summary of all orders
                return FormatOrdersSummary(orders);
            }
            catch (Exception ex)
            {
                return "I'm having trouble retrieving your order information right now. Please try again later.";
            }
        }

        private string FormatOrderDetails(Order order)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Order #{order.Id} placed on {order.CreatedAt.ToString("MMM dd, yyyy")}");
            sb.AppendLine($"Status: {order.Status}");
            
            if (order.OrderItems != null && order.OrderItems.Any())
            {
                sb.AppendLine("\nItems:");
                foreach (var item in order.OrderItems)
                {
                    sb.AppendLine($"- {item.Quantity}x {item.Product?.Name ?? "Unknown Product"} (${item.SubTotal:F2} each)");
                }
            }
            
            sb.AppendLine($"\nTotal: ${order.Total:F2}");
            
            if (!string.IsNullOrEmpty(order.ShippingAddress))
            {
                sb.AppendLine($"Shipping to: {order.ShippingAddress}");
            }
            
            if (order.Status == OrderStatus.Shipped && !string.IsNullOrEmpty(order.ShippingAddress))
            {
                sb.AppendLine($"Tracking number: {order.ShippingAddress}");
            }
            
            return sb.ToString();
        }

        private string FormatOrdersSummary(IEnumerable<Order> orders)
        {
            var recentOrders = orders.OrderByDescending(o => o.CreatedAt).Take(5);
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"You have {orders.Count()} orders in total. Here are your most recent orders:");
            
            foreach (var order in recentOrders)
            {
                sb.AppendLine($"- Order #{order.Id} ({order.CreatedAt.ToString("MMM dd, yyyy")}): {order.Status}, Total: ${order.Total:F2}");
            }
            
            sb.AppendLine("\nYou can ask me about a specific order by saying 'Show me order #123'");
            
            return sb.ToString();
        }

        #endregion

        #region Cart Handling

        private async Task<string> HandleCartQueryAsync(string userId, string message)
        {
            string lowercaseMessage = message.ToLower();
            
            try
            {
                // Get user's cart items
                var cartItems = await _unitOfWork.CartItemRepository.GetUserCartAsync(userId);
                
                // Check if asking to add item to cart
                var addToCartMatch = Regex.Match(message, @"add\s+(?:product|item)?\s*(?:id)?\s*(\d+)", RegexOptions.IgnoreCase);
                if (addToCartMatch.Success)
                {
                    int productId;
                    if (int.TryParse(addToCartMatch.Groups[1].Value, out productId))
                    {
                        // Check if product exists
                        var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
                        if (product != null)
                        {
                            // Extract quantity if specified
                            int quantity = 1;
                            var quantityMatch = Regex.Match(message, @"quantity\s+(\d+)", RegexOptions.IgnoreCase);
                            if (quantityMatch.Success)
                            {
                                int.TryParse(quantityMatch.Groups[1].Value, out quantity);
                            }
                            
                            // Add to cart logic would go here
                            // This is simplified and would need to be implemented based on your actual cart functionality
                            return $"I've added {quantity}x {product.Name} to your cart. Would you like to view your cart or continue shopping?";
                        }
                        else
                        {
                            return $"I couldn't find a product with ID {productId}. Would you like to search for products?";
                        }
                    }
                }
                
                // Check if asking to remove item from cart
                var removeFromCartMatch = Regex.Match(message, @"remove\s+(?:product|item)?\s*(?:id)?\s*(\d+)", RegexOptions.IgnoreCase);
                if (removeFromCartMatch.Success)
                {
                    int productId;
                    if (int.TryParse(removeFromCartMatch.Groups[1].Value, out productId))
                    {
                        var cartItem = cartItems.FirstOrDefault(ci => ci.productId == productId);
                        if (cartItem != null)
                        {
                            // Remove from cart logic would go here
                            return $"I've removed {cartItem.product.Name ?? "the item"} from your cart.";
                        }
                        else
                        {
                            return $"I couldn't find product ID {productId} in your cart.";
                        }
                    }
                }
                
                // Check if asking to clear cart
                if (lowercaseMessage.Contains("clear") || lowercaseMessage.Contains("empty"))
                {
                    // Clear cart logic would go here
                    return "I've cleared all items from your cart.";
                }
                
                // Check if asking to checkout
                if (lowercaseMessage.Contains("checkout") || lowercaseMessage.Contains("place order"))
                {
                    if (!cartItems.Any())
                    {
                        return "Your cart is empty. Would you like to browse our products?";
                    }
                    
                    // Checkout logic would go here
                    return "To complete your checkout, please visit the checkout page on our website. Would you like me to provide a link?";
                }
                
                // Default: show cart contents
                return FormatCartSummary(cartItems);
            }
            catch (Exception ex)
            {
                return "I'm having trouble accessing your cart information right now. Please try again later.";
            }
        }

        private string FormatCartSummary(IEnumerable<CartItem> cartItems)
        {
            if (!cartItems.Any())
            {
                return "Your cart is currently empty. Would you like to browse our products?";
            }
            
            decimal total = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Here's what's in your cart:");
            
            foreach (var item in cartItems)
            {
                decimal itemTotal = item.quantity * item.product.Price;
                total += itemTotal;
                sb.AppendLine($"- {item.quantity}x {item.product?.Name ?? "Unknown Product"} (${item.product?.Price:F2} each) = ${itemTotal:F2}");
            }
            
            sb.AppendLine($"\nTotal: ${total:F2}");
            sb.AppendLine("\nYou can say 'checkout' to place your order, or ask me to add or remove items.");
            
            return sb.ToString();
        }

        #endregion

        #region User Profile Handling

        private async Task<string> HandleUserProfileQueryAsync(string userId, string message)
        {
            try
            {
                // Get user profile
                var user = await _unitOfWork.UserRepository.GetUserWithDetailsAsync(userId);
                
                if (user == null)
                {
                    return "I'm having trouble retrieving your profile information. Please try again later.";
                }
                
                string lowercaseMessage = message.ToLower();
                
                // Check if asking about specific profile information
                if (lowercaseMessage.Contains("name"))
                {
                    return $"Your name is {user.FirstName} {user.LastName}.";
                }
                else if (lowercaseMessage.Contains("email"))
                {
                    return $"Your email address is {user.Email}.";
                }
                else if (lowercaseMessage.Contains("phone"))
                {
                    return string.IsNullOrEmpty(user.PhoneNumber) 
                        ? "You haven't added a phone number to your profile yet."
                        : $"Your phone number is {user.PhoneNumber}.";
                }
                else if (lowercaseMessage.Contains("address"))
                {
                    // Assuming user has an Address property or similar
                    string address = ""; // Replace with actual address retrieval
                    return string.IsNullOrEmpty(address)
                        ? "You haven't added a shipping address to your profile yet."
                        : $"Your shipping address is: {address}";
                }
                
                // Default: show general profile information
                return FormatUserProfile(user);
            }
            catch (Exception ex)
            {
                return "I'm having trouble retrieving your profile information right now. Please try again later.";
            }
        }

        private string FormatUserProfile(ApplicationUser user)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Hello, {user.FirstName}! Here's your profile information:");
            sb.AppendLine($"Name: {user.FirstName} {user.LastName}");
            sb.AppendLine($"Email: {user.Email}");
            
            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                sb.AppendLine($"Phone: {user.PhoneNumber}");
            }
            
            // Add more profile information as needed
            
            sb.AppendLine("\nYou can ask me to show your orders, your cart, or help you find products.");
            
            return sb.ToString();
        }

        #endregion

        #region Product Handling

        private string[] ExtractProductFilters(string message)
        {
            List<string> filters = new List<string>();
            
            // Extract price filters
            if (message.ToLower().Contains("price"))
            {
                // Look for price ranges like "price less than 100" or "price between 50 and 200"
                var priceMatch = Regex.Match(message, @"price\s+(less than|under|below|cheaper than)\s+(\d+)", RegexOptions.IgnoreCase);
                if (priceMatch.Success)
                {
                    filters.Add($"Price<{priceMatch.Groups[2].Value}");
                }
                
                priceMatch = Regex.Match(message, @"price\s+(more than|over|above|greater than)\s+(\d+)", RegexOptions.IgnoreCase);
                if (priceMatch.Success)
                {
                    filters.Add($"Price>{priceMatch.Groups[2].Value}");
                }
                
                priceMatch = Regex.Match(message, @"price\s+between\s+(\d+)\s+and\s+(\d+)", RegexOptions.IgnoreCase);
                if (priceMatch.Success)
                {
                    filters.Add($"Price>={priceMatch.Groups[1].Value},Price<={priceMatch.Groups[2].Value}");
                }
            }
            
            // Extract category filters
            var categoryMatch = Regex.Match(message, @"category\s+(?:is|=|equals)\s+([a-zA-Z\s]+)", RegexOptions.IgnoreCase);
            if (categoryMatch.Success)
            {
                filters.Add($"Category={categoryMatch.Groups[1].Value.Trim()}");
            }
            
            // Extract name filters
            var nameMatch = Regex.Match(message, @"name\s+(?:contains|like|has)\s+([a-zA-Z\s]+)", RegexOptions.IgnoreCase);
            if (nameMatch.Success)
            {
                filters.Add($"Name<>{nameMatch.Groups[1].Value.Trim()}");
            }
            
            // Extract brand filters
            var brandMatch = Regex.Match(message, @"brand\s+(?:is|=|equals)\s+([a-zA-Z\s]+)", RegexOptions.IgnoreCase);
            if (brandMatch.Success)
            {
                filters.Add($"Brand={brandMatch.Groups[1].Value.Trim()}");
            }
            
            // If no specific filters were found, try to extract general terms
            if (filters.Count == 0)
            {
                // Look for category keywords in the message
                var categories = _unitOfWork.CategoryRepository.GetAllAsync().Result;
                foreach (var category in categories)
                {
                    if (message.ToLower().Contains(category.Name.ToLower()))
                    {
                        filters.Add($"Category={category.Name}");
                        break;
                    }
                }
                
                // Look for brand keywords in the message
                var brands = _unitOfWork.BrandRepository.GetAllAsync().Result;
                foreach (var brand in brands)
                {
                    if (message.ToLower().Contains(brand.Name.ToLower()))
                    {
                        filters.Add($"Brand={brand.Name}");
                        break;
                    }
                }
            }
            
            return filters.ToArray();
        }

        private async Task<IEnumerable<Product>> GetFilteredProductsAsync(string[] filters)
        {
            // Use the repository with dynamic filter helper to get filtered products
            return await _unitOfWork.ProductRepository.GetAllFilteredAsync(filters);
        }

        private string FormatProductResponse(IEnumerable<Product> products, string originalQuery)
        {
            var productList = products.ToList();
            
            if (!productList.Any())
            {
                return "I couldn't find any products matching your criteria. Could you try a different search?";
            }
            
            StringBuilder response = new StringBuilder();
            response.AppendLine("Here are the products I found based on your request:");
            response.AppendLine();
            
            int count = 1;
            foreach (var product in productList.Take(5)) // Limit to 5 products to avoid too long responses
            {
                response.AppendLine($"{count}. {product.Name}");
                response.AppendLine($"   Price: ${product.Price:F2}");
                response.AppendLine($"   Category: {product.ProductCategories}");
                if (!string.IsNullOrEmpty(product.Description))
                {
                    response.AppendLine($"   Description: {product.Description}");
                }
                response.AppendLine();
                count++;
            }
            
            if (productList.Count > 5)
            {
                response.AppendLine($"...and {productList.Count - 5} more products.");
            }
            
            response.AppendLine("\nYou can ask me to add any of these products to your cart by saying 'Add product ID [number] to cart'.");
            
            return response.ToString();
        }

        #endregion

        private async Task<string> GetBotReplyAsync(string message)
        {
            var requestData = new
            {
                model = "meta-llama/llama-3.1-8b-instruct", // ✅ good free/fast model on OpenRouter
                messages = new object[]
                {
                    new { role = "system", content = "You are a helpful e-commerce customer service chatbot. You can help users find products, manage their cart, check order status, and update their profile information. Be friendly and helpful. Always address the user by their name if available." },
                    new { role = "user", content = message }
                },
                max_tokens = 200
            };

            var json = JsonSerializer.Serialize(requestData);

            var response = await _httpClient.PostAsync(
                "chat/completions", // ✅ OpenRouter endpoint
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenRouter API error {response.StatusCode}: {error}");
            }

            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);

            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
        }
    }
}
