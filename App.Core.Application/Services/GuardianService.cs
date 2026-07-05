using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Application.Interfaces;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;

namespace App.Core.Application.Services
{
    public class GuardianService : IGuardianService
    {
        private readonly IGenericRepository<Guardian> _guardianRepository;
        private readonly IPhoneNumberValidator _phoneNumberValidator;

        public GuardianService(IGenericRepository<Guardian> guardianRepository, IPhoneNumberValidator phoneNumberValidator)
        {
            _guardianRepository = guardianRepository ?? throw new ArgumentNullException(nameof(guardianRepository));
            _phoneNumberValidator = phoneNumberValidator ?? throw new ArgumentNullException(nameof(phoneNumberValidator));
        }

        public async Task<Guardian?> GetByIdAsync(Guid id)
        {
            return await _guardianRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyCollection<Guardian>> GetAllAsync(bool includeInactive = false)
        {
            return await _guardianRepository.GetAllAsync(includeInactive);
        }

        public async Task AddAsync(Guardian guardian)
        {
            if (guardian is null)
            {
                throw new ArgumentException("El encargado no puede estar vacío.", nameof(guardian));
            }

            if (guardian.PhoneNumbers == null || !guardian.PhoneNumbers.Any())
            {
                throw new ArgumentException("El encargado debe tener al menos un número de teléfono.", nameof(guardian));
            }

            if (!_phoneNumberValidator.ValidateNumber(guardian.PhoneNumbers.First().Number))
            {
                throw new ArgumentException("El formato del número de teléfono no es válido.", nameof(guardian));
            }

            await _guardianRepository.AddAsync(guardian);
        }

        public async Task UpdateAsync(Guardian guardian)
        {
            if (guardian is null)
            {
                throw new ArgumentException("El encargado no puede estar vacío.", nameof(guardian));
            }

            if (guardian.PhoneNumbers == null || !guardian.PhoneNumbers.Any())
            {
                throw new ArgumentException("El encargado debe tener al menos un número de teléfono.", nameof(guardian));
            }

            if (!_phoneNumberValidator.ValidateNumber(guardian.PhoneNumbers.First().Number))
            {
                throw new ArgumentException("El formato del número de teléfono no es válido.", nameof(guardian));
            }

            var existingGuardian = await _guardianRepository.GetByIdAsync(guardian.Id);
            if (existingGuardian is null)
            {
                throw new KeyNotFoundException("No se encontró el encargado que se desea actualizar.");
            }

            await _guardianRepository.UpdateAsync(guardian);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var existingGuardian = await _guardianRepository.GetByIdAsync(id);
            if (existingGuardian is null)
            {
                throw new KeyNotFoundException("No se encontró el encargado que se desea desactivar.");
            }

            await _guardianRepository.DeactiveAsync(id);
        }
    }
}