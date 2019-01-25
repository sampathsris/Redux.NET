using CounterExampleCore;
using Redux;
using Redux.Primitives;
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

        IStore<int> store;

        private void SetupReduxStore()
        {
            store = Ops<int>.CreateStore(Counter.Reduce);
            store.StateChanged += StoreStateChanged;
            SetLabel(store.State);
        }

        private void StoreStateChanged(object sender, int state)
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
