using CarMatt.Data.DTOs.AgentModule;
using CarMatt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.AgentModule
{
    public class AgentService : IAgentService
    {
        private readonly ApplicationDbContext context;

        public AgentService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<AgentDTO> Create(AgentDTO agentDTO)
        {
            try
            {
                var s = new Agent
                {
                    Id = Guid.NewGuid(),

                    FirstName = agentDTO.FirstName,

                    LastName = agentDTO.LastName,

                    Email = agentDTO.Email,

                    PhoneNumber = agentDTO.PhoneNumber,

                    Gender = agentDTO.Gender,

                    Town = agentDTO.Town,

                    County = agentDTO.County,

                    CreatedBy = agentDTO.CreatedBy,

                    CreateDate = DateTime.Now
                };

                context.Agents.Add(s);

                await context.SaveChangesAsync();

                return agentDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.Agents.FindAsync(Id);

                if (s != null)
                {
                    context.Agents.Remove(s);

                    await context.SaveChangesAsync();

                    return true;
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<AgentDTO>> GetAll()
        {
            try
            {
                var agent = await context.Agents.ToListAsync();

                var agents = new List<AgentDTO>();

                foreach (var item in agent)
                {
                    var data = new AgentDTO()
                    {
                        Id = item.Id,

                        FirstName = item.FirstName,

                        LastName = item.LastName,

                        Email = item.Email,

                        PhoneNumber = item.PhoneNumber,

                        Gender = item.Gender,

                        Town = item.Town,

                        County = item.County,

                        CreatedBy = item.CreatedBy,

                        CreateDate = item.CreateDate
                    };

                    agents.Add(data);
                }

                return agents;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<AgentDTO> GetById(Guid Id)
        {
            try
            {
                var agent = await context.Agents.FindAsync(Id);

                return new AgentDTO()
                {
                    Id = agent.Id,

                    FirstName = agent.FirstName,

                    LastName = agent.LastName,

                    Email = agent.Email,

                    PhoneNumber = agent.PhoneNumber,

                    Gender = agent.Gender,

                    Town = agent.Town,

                    County = agent.County,

                    CreatedBy = agent.CreatedBy,

                    CreateDate = agent.CreateDate
                };


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<AgentDTO> Update(AgentDTO agentDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Agents.FindAsync(agentDTO.Id);

                    s.FirstName = agentDTO.FirstName;

                    s.LastName = agentDTO.LastName;

                    s.Email = agentDTO.Email;

                    s.PhoneNumber = agentDTO.PhoneNumber;

                    s.Gender = agentDTO.Gender;

                    s.Town = agentDTO.Town;

                    s.County = agentDTO.County;

                    transaction.Commit();

                }
                await context.SaveChangesAsync();

                return agentDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
