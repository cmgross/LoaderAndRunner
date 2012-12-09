using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoaderAndRunner
{
    public partial class Form1 : Form
    {
        private BlockingCollection<ListViewItem> _itemsToLoad;
        private BlockingCollection<ListViewItem> _itemsToRun;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _itemsToLoad = new BlockingCollection<ListViewItem>();
            _itemsToRun = new BlockingCollection<ListViewItem>();

            var item1 = new ListViewItem("Item1");
            var item2 = new ListViewItem("Item2");
            var item3 = new ListViewItem("Item3");
            var item4 = new ListViewItem("Item4");
            

            itemsToDo.Items.Add(item1);
            itemsToDo.Items.Add(item2);
            itemsToDo.Items.Add(item3);
            itemsToDo.Items.Add(item4);
            _itemsToLoad.Add(item1);
            _itemsToLoad.Add(item2);
            _itemsToLoad.Add(item3);
            _itemsToLoad.Add(item4);

            _itemsToLoad.CompleteAdding();
        }

        private void run_Click(object sender, EventArgs e)
        {
            StartBackgroundTasks();
            TaskIsRunning();
        }

        private void StartBackgroundTasks()
        {
            Task loader1 = Task.Factory.StartNew(() =>
            {
                foreach (ListViewItem item in _itemsToLoad.GetConsumingEnumerable())
                {
                    //consuming an item should remove it from the collection. 
                    //since _itemsToLoad is set as complete adding, once this list has been enumerated through it should gracefully exit

                    //simulate object loading from database that is long running
                    Debug.WriteLine("Loader 1 says: loading item: " + item.Text + " started at: " + DateTime.Now);
                    Thread.Sleep(8000);
                    Debug.WriteLine("Loader 1 says: loading item: " + item.Text + " completed at " + DateTime.Now);
                    _itemsToRun.Add(item);
                    loading.Invoke(new MethodInvoker(() => loading.PerformStep()));
                }
                Debug.WriteLine("Loader 1 says: Nothing left to load!");
            }, TaskCreationOptions.LongRunning);

            Task loader2 = Task.Factory.StartNew(() =>
            {
                foreach (ListViewItem item in _itemsToLoad.GetConsumingEnumerable())
                {
                    //consuming an item should remove it from the collection. 
                    //since _itemsToLoad is set as complete adding, once this list has been enumerated through it should gracefully exit

                    //simulate loading from database that is long running
                    Debug.WriteLine("Loader 2 says: loading item: " + item.Text + " started at: " + DateTime.Now);
                    Thread.Sleep(8000);
                    Debug.WriteLine("Loader 2 says: loading item: " + item.Text + " completed at " + DateTime.Now);
                    _itemsToRun.Add(item);
                    loading.Invoke(new MethodInvoker(() => loading.PerformStep()));
                }
                Debug.WriteLine("Loader 2 says: nothing left to load!");
            }, TaskCreationOptions.LongRunning);

            Task runner1 = Task.Factory.StartNew(() =>
            {
                foreach (ListViewItem item in _itemsToRun.GetConsumingEnumerable())
                {
                    //consuming an item should remove it from the collection. 
                    //since _itemsToRun is set as complete adding, once this list has been enumerated through it should gracefully exit

                    //simulate writing information to mainframe emulator
                    Debug.WriteLine("Runner 1 says: running item: " + item.Text + " started at: " + DateTime.Now);
                    Thread.Sleep(1000);
                    Debug.WriteLine("Runner 1 says: running item: " + item.Text + " completed at " + DateTime.Now);

                    ListViewItem item1 = item;
                    itemsToDo.Invoke(new MethodInvoker(() => itemsToDo.Items.Remove(item1)));
                    Debug.WriteLine("Runner 1 removed item: " + item1.Text + " from ToDo Listview at " + DateTime.Now);
                    itemsCompleted.Invoke(new MethodInvoker(() => itemsCompleted.Items.Add(item1)));
                    Debug.WriteLine("Runner 1 added item: " + item1.Text + " to Completed Listview at " + DateTime.Now);
                    running.Invoke(new MethodInvoker(() => running.PerformStep()));
                }
                Debug.WriteLine("Runner 1 says: Nothing left to run!");
            }, TaskCreationOptions.LongRunning);

            //Task.WaitAll(loader1, loader2, runner1);
            Task.Factory.ContinueWhenAll(new[] { loader1, loader2 }, completedTasks => _itemsToRun.CompleteAdding());
            Task.Factory.ContinueWhenAll(new[] { runner1 }, completedTasks => TaskIsComplete());
        }

        private void TaskIsRunning()
        {
            // Update UI to reflect background task.
            run.Enabled = false;

            lbLoading.Visible = true;
            loading.Visible = true;
            loading.Minimum = 0;
            loading.Maximum = 4;
            loading.Value = 0;
            loading.Step = 1;

            lbRunning.Visible = true;
            running.Visible = true;
            running.Minimum = 0;
            running.Maximum = 4;
            running.Value = 0;
            running.Step = 1;
        }

        private void TaskIsComplete()
        {
            // Reset UI.
            run.Invoke(new MethodInvoker(() => run.Enabled = true));
            //loading.Invoke(new MethodInvoker(() => loading.Visible = false));
        }
    }
}
