using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.NutritionFact;

namespace Company.Project.Application.Services
{
    public class NutritionFactService : INutritionFactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NutritionFactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NutritionFact>> GetAllAsync(int skip, int take)
        {
            return await _unitOfWork.NutritionFactRepository.GetAllAsync(skip, take); 
        }

        public async Task<NutritionFact> GetByIdAsync(int id)
        {
            var nutritionFact = await _unitOfWork.NutritionFactRepository.GetByIdAsync(id);
            if (nutritionFact == null)
            {
                throw new KeyNotFoundException("Nutrition fact not found.");
            }
            return nutritionFact;
        }

        public async Task<NutritionFact> CreateAsync(CreateNutritionFactDto createDto)
        {
            var nutritionFact = new NutritionFact
            {
                Nutrient = createDto.Nutrient,
                Amount = createDto.Amount,
                DailyValue = createDto.DailyValue,
                ProductId = createDto.ProductId
            };
            await _unitOfWork.NutritionFactRepository.AddAsync(nutritionFact);
            await _unitOfWork.Completeasync();
            return await Task.FromResult(nutritionFact);
        }

        public async Task<NutritionFact> UpdateAsync(UpdateNutritionFactDto updateDto)
        {
            var existingItem = _unitOfWork.NutritionFactRepository.GetByIdAsync(updateDto.Id).Result;
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Nutrition fact not found.");
            }
            existingItem.Nutrient = updateDto.Nutrient;
            existingItem.Amount = updateDto.Amount;
            existingItem.DailyValue = updateDto.DailyValue;
            await _unitOfWork.NutritionFactRepository.UpdateAsync(existingItem);
            await _unitOfWork.Completeasync();
            return await Task.FromResult(existingItem);
        }

        public async Task DeleteAsync(int id)
        {
            var existingItem = _unitOfWork.NutritionFactRepository.GetByIdAsync(id).Result;
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Nutrition fact not found.");
            }
            await _unitOfWork.NutritionFactRepository.DeleteAsync(existingItem);
            await _unitOfWork.Completeasync();
            await  Task.CompletedTask;
            return;
        }
    }
}