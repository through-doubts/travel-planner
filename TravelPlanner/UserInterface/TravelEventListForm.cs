using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.UserInterface
{
    sealed class TravelEventListForm : ChooseOptionForm<ITravelEvent>
    {
        private readonly IApplication app;

        public TravelEventListForm(Func<List<ITravelEvent>> getOptions, string text,
            IApplication app) : base(getOptions, () => app)
        {
            this.app = app;
            Text = text;
            Size = new Size(800, 600);
        }

        protected override List<Button> GetButtons()
        {
            return new List<Button>
            {
                Elements.BackButton(this, "Назад")
            };
        }

        protected override Button GetOptionButton(ITravelEvent option, Func<IApplication> getApp = null)
        {
            return Elements.GetButton(option.ToStringValue(), ((sender, args) =>
            {
                app.UserSessionHandler.CurrentTravelEvents.Add(option);
                DialogResult = DialogResult.OK;
                Close();
            }));
        }
    }
}