using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{

    public class Process // 
    {
        public int Id { get; set; }
        public int BurstTime { get; set; }
        public int Priority { get; set; }
        public int WaitTime { get; set; }
        public int TurnaroundTime { get; set; }

        public int Deadline { get; set; }



        public Process(int id, int burstTime)
        {
            Id = id;
            BurstTime = burstTime;
            WaitTime = 0;
            TurnaroundTime = 0;
        }

        public Process (int id, int burst, int deadline)
        {
            Id = id;
            BurstTime = burst;
            WaitTime = 0;
            TurnaroundTime = 0;
            Deadline = deadline;
        }
    }


    public class Algorithms_2
    {
        BankerAlgorithm bankerAlgorithm;
        Random rnd = new Random();



        public void BankersAlgorithm(string userInput)
        {
            int num_process = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Bankers Algorithm with Earliest Deadline First Schedulinng", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {

                Queue<Process> deadlineQueue = new Queue<Process>();
                bankerAlgorithm = new BankerAlgorithm(num_process, (num_process * rnd.Next(1, 4)));

                for (int i = 0; i < num_process; i++)
                {

                    string input = Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ", "Burst time for P" + (i + 1), "", -1, -1);

                    string input2 = Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ", "Priority for P" + (i + 1), "", -1, -1);

                    deadlineQueue.Enqueue(new Process(i + 1, int.Parse(input), int.Parse(input2)));
                }


                int currentTime = 0;

                // Sort processes by deadline (Earliest Deadline First)
                var sortedQueue = deadlineQueue.OrderBy(p => p.Deadline).ToList();

                foreach (var process in sortedQueue)
                {
                    process.WaitTime = Math.Max(0, currentTime - process.Deadline);
                    currentTime += process.BurstTime;
                    process.TurnaroundTime = process.WaitTime + process.BurstTime;
                    MessageBox.Show("Process " + process.Id + " Wait Time " + process.WaitTime + " Turnaround Time " + process.TurnaroundTime + " Deadline " + process.Deadline);
                }

            }


        }
       
        public void MultiLevelQueue(string userInput)
        {
            int num_process = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Multi-level Queue Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                    Queue<Process> highPriorityQueue = new Queue<Process>();
                    Queue<Process> mediumPriorityQueue = new Queue<Process>();
                    Queue<Process> lowPriorityQueue = new Queue<Process>();

                for (int num = 0; num <= num_process - 1; num++)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ", "Burst time for P" + (num + 1),"",-1, -1);
                    int int_input = int.Parse(input);
                    if (int_input <= 3)
                    {
                        lowPriorityQueue.Enqueue(new Process ( (num), int_input ));
                    }
                    else if (int_input > 3 && int_input <= 7)
                    {
                        mediumPriorityQueue.Enqueue(new Process((num), int_input));
                    }
                    else {
                        highPriorityQueue.Enqueue(new Process((num), int_input));
                    }
                }

                MessageBox.Show("Starting Multi-Level Queue Scheduler...");

                // High priority queue - Round Robin
                MessageBox.Show("\nHigh Priority Queue (Round Robin):");
                RoundRobin(highPriorityQueue);

                // Medium priority queue - FCFS
                MessageBox.Show("\nMedium Priority Queue (FCFS):");
                FCFS(mediumPriorityQueue);

                // Low priority queue - FCFS with feedback mechanism for aging
                MessageBox.Show("\nLow Priority Queue (FCFS with Feedback):");
                SJF(lowPriorityQueue);

            }

        }

        /// <summary>
        /// For this method I have to implement another attrbute to my process class called deadline. Because 
        /// if i were to go the lazy route I could use tge burst time as an indicator for the deadline. But havinng those
        /// two attributes seperate can prve useful if this implementation were to change.
        /// </summary>
        /// <param name="userInput"></param>
        public void EarliestDeadlineFirst(string userInput)
        {
            int num_process = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Earliest Deadline First", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Queue<Process> deadlineQueue = new Queue<Process>();

                for (int i = 0; i < num_process; i++) {

                    string input = Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ", "Burst time for P" + (i + 1), "", -1, -1);

                    string input2 = Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ","Priority for P" + (i + 1),"",-1, -1);

                    deadlineQueue.Enqueue(new Process(i + 1, int.Parse(input), int.Parse(input2)));
                }

                if (bankerAlgorithm.IsSafeState(out List<int> safeSequence))
                {

                    MessageBox.Show("Safe State! Executing Shortest Deadline First Scheduling.");
                    int currentTime = 0;

                    // Sort processes by deadline (Earliest Deadline First)
                    var sortedQueue = deadlineQueue.OrderBy(p => p.Deadline).ToList();

                    foreach (var process in sortedQueue)
                    {
                        process.WaitTime = Math.Max(0, currentTime - process.Deadline);
                        currentTime += process.BurstTime;
                        process.TurnaroundTime = process.WaitTime + process.BurstTime;
                        MessageBox.Show("Process " + process.Id + " Wait Time " + process.WaitTime + " Turnaround Time " + process.TurnaroundTime + " Deadline " + process.Deadline);
                    }
                }
                else
                {
                    MessageBox.Show("Unsafe State detected. Resolving Deadlock...");
                    bankerAlgorithm.ResolveDeadlock();
                }
            }


        }

        //------------------These are all helper methods to aid the main methods Im implementing--------------//
        public void FCFS(Queue<Process> queue)
        {
            int currentTime = 0;

            for(int i = 0; i < queue.Count; i ++)
            {
                Process process = queue.Dequeue();
                process.WaitTime = currentTime;
                currentTime += process.BurstTime;
                process.TurnaroundTime = process.WaitTime + process.BurstTime;
                MessageBox.Show("Process" + process.Id + ": Wait Time" + process.WaitTime + " Turnaround Time " + process.TurnaroundTime);
            }

        }

        public void SJF(Queue<Process> queue)
        {
            int currentTime = 0;

            // Sort processes by burst time (Shortest Job First)
            var sortedQueue = queue.OrderBy(p => p.BurstTime).ToList();

            foreach (var process in sortedQueue)
            {
                process.WaitTime = currentTime;
                currentTime += process.BurstTime;
                process.TurnaroundTime = process.WaitTime + process.BurstTime;
                MessageBox.Show("Process" + process.Id + ": Wait Time" + process.WaitTime + " Turnaround Time " + process.TurnaroundTime); 
            }

        }

        public void RoundRobin(Queue<Process> queue)
        {
             int currentTime = 0;
             int timeQuantum = 2;

            for (int i = 0; i < queue.Count; i++) {
                Process process = queue.Dequeue();
                int remainingTime = process.BurstTime - timeQuantum;

                if (remainingTime > 0)
                {
                    currentTime += timeQuantum; // Used for calculating wait time and turnaround
                    process.BurstTime -= timeQuantum;
                    queue.Enqueue(process); // Re-enqueue process for further CPU time
                }
                else
                {
                    currentTime += process.BurstTime;
                    process.WaitTime = currentTime - process.BurstTime;
                    process.TurnaroundTime = process.WaitTime + process.BurstTime;
                    MessageBox.Show("Process" + process.Id + ": Wait Time" + process.WaitTime + " Turnaround Time " + process.TurnaroundTime);

                }

            }
        }

    }

    public class BankerAlgorithm
    {
        private int[,] allocation;
        private int[,] max;
        private int[,] need;
        private int[] available;

        private int numProcesses;
        private int numResources;

        public BankerAlgorithm(int numProcesses, int numResources)
        {
            this.numProcesses = numProcesses;
            this.numResources = numResources;
            this.allocation = GenerateRandomAllocation();
            this.max = GenerateRandomMax();
            this.available = GenerateRandomAvailable();
            this.need = CalculateNeed();
        }

        private int[,] GenerateRandomAllocation()
        {
            Random random = new Random();
            int[,] allocationMatrix = new int[numProcesses, numResources];
            for (int i = 0; i < numProcesses; i++)
                for (int j = 0; j < numResources; j++)
                    allocationMatrix[i, j] = random.Next(0, available[j] + 1);
            return allocationMatrix;
        }

        private int[,] GenerateRandomMax()
        {
            Random random = new Random();
            int[,] maxMatrix = new int[numProcesses, numResources];
            for (int i = 0; i < numProcesses; i++)
                for (int j = 0; j < numResources; j++)
                    maxMatrix[i, j] = allocation[i, j] + random.Next(1, 4); // Max is higher than allocation
            return maxMatrix;
        }

        private int[] GenerateRandomAvailable()
        {
            Random random = new Random();
            int[] availableResources = new int[numResources];
            for (int i = 0; i < numResources; i++)
                availableResources[i] = random.Next(1, 10); // Randomly set available resources
            return availableResources;
        }

        private int[,] CalculateNeed()
        {
            int[,] needMatrix = new int[numProcesses, numResources];
            for (int i = 0; i < numProcesses; i++)
                for (int j = 0; j < numResources; j++)
                    needMatrix[i, j] = max[i, j] - allocation[i, j];
            return needMatrix;
        }

        public bool IsSafeState(out List<int> safeSequence)
        {
            safeSequence = new List<int>();
            bool[] finished = new bool[numProcesses];
            int[] work = (int[])available.Clone();

            for (int count = 0; count < numProcesses; count++)
            {
                bool found = false;
                for (int i = 0; i < numProcesses; i++)
                {
                    if (!finished[i] && CanProcessExecute(i, work))
                    {
                        for (int j = 0; j < numResources; j++)
                            work[j] += allocation[i, j];
                        safeSequence.Add(i);
                        finished[i] = true;
                        found = true;
                    }
                }

                if (!found) return false;
            }
            return true;
        }


        private bool CanProcessExecute(int i, int[] work)
        {
            for (int j = 0; j < numResources; j++)
                if (need[i, j] > work[j])
                    return false;
            return true;
        }

        public void SimulateDeadlock()
        {
            allocation[0, 0] = 1;
            allocation[1, 1] = 2;
            allocation[2, 2] = 1;

            available[0] = 0;
            available[1] = 0;
            available[2] = 0;

            MessageBox.Show("Simulated Deadlock Scenario.");
        }

        public void ResolveDeadlock()
        {
            if (!IsSafeState(out _))
            {
                MessageBox.Show("Deadlock detected! Resolving...");

                for (int i = numProcesses - 1; i >= 0; i--)
                {
                    MessageBox.Show("Attempting to release resources from Process" + i);
                    for (int j = 0; j < numResources; j++)
                        available[j] += allocation[i, j];

                    allocation[i, 0] = 0;
                    allocation[i, 1] = 0;
                    allocation[i, 2] = 0;

                    if (IsSafeState(out List<int> newSafeSequence))
                    {
                        MessageBox.Show("Deadlock resolved! Safe Sequence: " + string.Join(" -> ", newSafeSequence));
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Releasing resources from Process" + i + " did not resolve deadlock. Continuing...");
                    }
                }

                MessageBox.Show("Could not resolve deadlock by resource preemption.");
            }
            else
            {
                MessageBox.Show("No deadlock detected.");
            }
        }
    }
}


