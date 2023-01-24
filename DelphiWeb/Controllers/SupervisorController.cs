using Microsoft.AspNetCore.Mvc;
using DelphiSupervisorV6;
using System.IO;
using AutoMapper;
using SupervisorLogic;

namespace DelphiWeb.Controllers
{
    [Route("/api")]
    public class SupervisorController : Controller
    {
        private IConfigWatcher _configWatcher;
        private IProcessProvider _processProvider;

        private readonly IMapper _mapper;

        public SupervisorController(IConfigWatcher configWatcher, IProcessProvider processProvider, IMapper mapper)
        {
            _configWatcher = configWatcher;
            _processProvider = processProvider;
            _mapper = mapper;
        }

        [HttpGet("processes")]
        public IActionResult GetAll()
        {
            var processes = _processProvider.GetAllProcesses();

            if (processes == null)
            {
                return BadRequest("bad request");
            }

            return Ok(processes.Select(process => _mapper.Map<ProcessInfoDto>(process)));
        }

        [HttpGet("processes/processId")]
        public IActionResult GetById(int id)
        {
            var process = _processProvider.GetByID(id);

            if(process == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<ProcessInfoDto>(process));
        }

        [HttpGet("processes/processName")]
        public IActionResult GetByName(string name)
        {
            var processes = _processProvider.GetProcessByName(name);

            if(processes == null)
            {
                return BadRequest();
            }

            return Ok(processes.Select(process => _mapper.Map<ProcessInfoDto>(process)));
        }

        [HttpPost("processes/command/executeCommand")]
        public IActionResult ExecuteCommand(Command command)
        {
            if(command.CommandType == CommandType.Start)
            {
                var process = _processProvider.StartProcces(command.Path);

                if (process == null)
                {
                    return BadRequest();
                }

                return Ok(_mapper.Map<ProcessInfoDto>(process));
            }

            if(command.CommandType == CommandType.Kill)
            {
                var process = _processProvider.KillProcces(command.ProcessId);

                if (process == null)
                {
                    return BadRequest();
                }

                return Ok(_mapper.Map<ProcessInfoDto>(process));
            }

            return BadRequest();          
        }

        [HttpGet("processes/processConfigured")]
        public IActionResult GetConfigureService()
        {
            _configWatcher.AddConfigureServices();
            var process = _processProvider.GetAllConfigureServices();

            if (process == null)
            {
                return BadRequest();
            }

            return Ok(process.Select(process => _mapper.Map<ProcessInfoDto>(process)));
        }
    }
}
