
using CarMatt.Data.DTOs.AgentModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.AgentModule
{
    public interface IAgentService
    {
        Task<AgentDTO> Create(AgentDTO agentDTO);
        Task<AgentDTO> Update(AgentDTO agentDTO);
        Task<List<AgentDTO>> GetAll();
        Task<AgentDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
    }
}