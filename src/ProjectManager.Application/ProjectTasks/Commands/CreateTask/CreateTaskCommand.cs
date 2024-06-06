//using MediatR;
//using ProjectManager.Application.interfaces;
//using System.Threading.Tasks;
//using System.Threading;

//namespace ProjectManager.Application.ProjectTasks.Commands
//{
//    public class CreateTaskCommand : IRequest<bool>
//    {
//        public CreateTaskCommand()
//        {
//        }
//    }

//    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, bool>
//    {
//        private readonly IAppDbContext _context;
//        public CreateTaskCommandHandler(IAppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<bool> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
//        {

//        }
//    }
//}