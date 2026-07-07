using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Application.DTOs;
using App.Core.Application.DTOs.Guardians;
using App.Core.Application.Interfaces;
using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using AutoMapper;

namespace App.Core.Application.Services
{
    public class GuardianService : IGuardianService
    {
        private readonly IGenericRepository<Guardian> _guardianRepository;
        private readonly IPhoneNumberValidator _phoneNumberValidator;
        private readonly IMapper _mapper;

        public GuardianService(IGenericRepository<Guardian> guardianRepository, IPhoneNumberValidator phoneNumberValidator, IMapper mapper)
        {
            _guardianRepository = guardianRepository ?? throw new ArgumentNullException(nameof(guardianRepository));
            _phoneNumberValidator = phoneNumberValidator ?? throw new ArgumentNullException(nameof(phoneNumberValidator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GuardianDto?> GetByIdAsync(Guid id)
        {
            var guardian = await _guardianRepository.GetByIdAsync(id);
            return guardian != null ? _mapper.Map<GuardianDto>(guardian) : null;
        }

        public async Task<IReadOnlyCollection<GuardianDto>> GetAllAsync(bool includeInactive = false)
        {
            var guardians = await _guardianRepository.GetAllAsync(includeInactive);
            return _mapper.Map<IReadOnlyCollection<GuardianDto>>(guardians);
        }

        public async Task AddAsync(CreateGuardianDto createGuardianDto)
        {
            if (createGuardianDto is null)
            {
                throw new ArgumentException("El encargado no puede estar vacío.", nameof(createGuardianDto));
            }

            if (createGuardianDto.PhoneNumbers == null || !createGuardianDto.PhoneNumbers.Any())
            {
                throw new ArgumentException("El encargado debe tener al menos un número de teléfono.", nameof(createGuardianDto));
            }

            foreach (PhoneNumberDto pNumber in createGuardianDto.PhoneNumbers)
            {
                if (!_phoneNumberValidator.ValidateNumber(pNumber.Number))
                {
                    throw new ArgumentException("El formato del número de teléfono no es válido.", nameof(createGuardianDto));
                }
            }
 

            await _guardianRepository.AddAsync(_mapper.Map<Guardian>(createGuardianDto));
        }

        public async Task UpdateAsync(UpdateGuardianDto updateGuardianDto)
        {
            if (updateGuardianDto is null)
            {
                throw new ArgumentException("El encargado no puede estar vacío.", nameof(updateGuardianDto));
            }

            if (updateGuardianDto.PhoneNumbers == null || !updateGuardianDto.PhoneNumbers.Any())
            {
                throw new ArgumentException("El encargado debe tener al menos un número de teléfono.", nameof(updateGuardianDto));
            }

            foreach (PhoneNumberDto pNumber in updateGuardianDto.PhoneNumbers)
            {
                if (!_phoneNumberValidator.ValidateNumber(pNumber.Number))
                {
                    throw new ArgumentException("El formato del número de teléfono no es válido.", nameof(updateGuardianDto));
                }
            }

            var existingGuardian = await _guardianRepository.GetByIdAsync(updateGuardianDto.Id);
            if (existingGuardian is null)
            {
                throw new KeyNotFoundException("No se encontró el acudiente que se desea actualizar.");
            }

            _mapper.Map(updateGuardianDto, existingGuardian);
            await _guardianRepository.UpdateAsync(existingGuardian);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var existingGuardian = await _guardianRepository.GetByIdAsync(id);
            if (existingGuardian is null)
            {
                throw new KeyNotFoundException("No se encontró el acudiente que se desea desactivar.");
            }

            await _guardianRepository.DeactiveAsync(id);
        }
    }
}