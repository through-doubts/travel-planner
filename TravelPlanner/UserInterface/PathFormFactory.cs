using TravelPlanner.Application;

namespace TravelPlanner.UserInterface
{
    class PathFormFactory
    {
        private readonly IApplication app;
        private readonly TravelEventFormFactory addFormFactory;

        public PathFormFactory(IApplication app, TravelEventFormFactory addFormFactory)
        {
            this.app = app;
            this.addFormFactory = addFormFactory;
        }

        public PathForm CreatePathForm()
        {
            return new PathForm(app, addFormFactory);
        }
    }
}