using CarMatt.Data.DTOs.InquiryModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.InquiryModule
{
    public interface IInquiryService
    {
        Task<InquiryDTO> Create(InquiryDTO  inquiryDTO);
        Task<InquiryDTO> Update(InquiryDTO  inquiryDTO);
        Task<bool> Delete(Guid Id);
        Task<List<InquiryDTO>> GetAll();
        Task<InquiryDTO> GetById(Guid Id);
    }
}