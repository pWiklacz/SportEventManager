using Autofac;
using SportEventManager.Core.Interfaces;

namespace SportEventManager.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {//below the example of Services adding
    //builder.RegisterType<ToDoItemSearchService>()
    //    .As<IToDoItemSearchService>().InstancePerLifetimeScope();
    //
    //builder.RegisterType<DeleteContributorService>()
    //    .As<IDeleteContributorService>().InstancePerLifetimeScope();
  }
}
