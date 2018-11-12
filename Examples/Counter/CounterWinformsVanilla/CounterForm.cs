using CounterExampleCore;
using Redux;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CounterWinformsVanilla
{
    public partial class CounterForm : Form
    {
        public CounterForm()
        {
            InitializeComponent();
            SetupReduxStore();
        }

        IStore store;

        private void SetupReduxStore()
        {
            store = Ops.CreateStore<int>(Counter.Reduce);
            store.Subscribe<int>(StoreStateChanged);
            SetLabel(store.GetState<int>());
        }

        private void StoreStateChanged(IStore store, int state)
        {
            SetLabel(state);
        }

        private void SetLabel(int count)
        {
            lblCount.Text = count.ToString();
        }

        private void btnDecrement_Click(object sender, EventArgs e)
        {
            store.Dispatch(Counter.Decrement());
        }

        private void btnIncrement_Click(object sender, EventArgs e)
        {
            store.Dispatch(Counter.Increment());
        }
    }
}
