using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int num_of_processes = Convert.ToInt16(userInput);
            int num_of_processesX2 = num_of_processes * 2;

            double[] burst_time_4process = new double[num_of_processes];
            double[] wait_time_4process = new double[num_of_processes];
            string[] output1 = new string[num_of_processesX2];
            double total_wait_time = 0.0, average_wait_time;
            int process;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (process = 0; process <= num_of_processes - 1; process++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (process + 1),
                                                       "",
                                                       -1, -1);

                    burst_time_4process[process] = Convert.ToInt64(input);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }

                for (process = 0; process <= num_of_processes - 1; process++)
                {
                    if (process == 0)
                    {
                        wait_time_4process[process] = 0;
                    }
                    else
                    {
                        wait_time_4process[process] = wait_time_4process[process - 1] + burst_time_4process[process - 1];
                        MessageBox.Show("Waiting time for P" + (process + 1) + " = " + wait_time_4process[process], "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                for (process = 0; process <= num_of_processes - 1; process++)
                {
                    total_wait_time = total_wait_time + wait_time_4process[process];
                }
                average_wait_time = total_wait_time / num_of_processes;
                MessageBox.Show("Average waiting time for " + num_of_processes + " processes" + " = " + average_wait_time + " sec(s)", "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int num_Oprocess = Convert.ToInt16(userInput);

            double[] burst_Oprocess = new double[num_Oprocess];
            double[] wait_time_Oprocess = new double[num_Oprocess];
            double[] priority_Oprocess = new double[num_Oprocess];
            double twt = 0.0, awt;
            int target, process_Oindex;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (process_Oindex = 0; process_Oindex <= num_Oprocess - 1; process_Oindex++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (process_Oindex + 1),
                                                           "",
                                                           -1, -1);

                    burst_Oprocess[process_Oindex] = Convert.ToInt64(input);
                }
                for (process_Oindex = 0; process_Oindex <= num_Oprocess - 1; process_Oindex++)
                {
                    priority_Oprocess[process_Oindex] = burst_Oprocess[process_Oindex];
                }
                for (target = 0; target <= num_Oprocess - 2; target++) // IF C# HAS AN ARRAYS.SORT FUNCTION THIS COULD BE QUICKLY IMPLEMENTED AND PROBABLY REDUCE BULK HERE.
                {
                    for (process_Oindex = 0; process_Oindex <= num_Oprocess - 2; process_Oindex++)
                    {
                        if (priority_Oprocess[process_Oindex] > priority_Oprocess[process_Oindex + 1])
                        {
                            temp = priority_Oprocess[process_Oindex];
                            priority_Oprocess[process_Oindex] = priority_Oprocess[process_Oindex + 1];
                            priority_Oprocess[process_Oindex + 1] = temp;
                        }
                    }
                }
                for (process_Oindex = 0; process_Oindex <= num_Oprocess - 1; process_Oindex++)
                {
                    if (process_Oindex == 0)
                    {
                        for (target = 0; target <= num_Oprocess - 1; target++)
                        {
                            if (priority_Oprocess[process_Oindex] == burst_Oprocess[target] && found == false)
                            {
                                wait_time_Oprocess[process_Oindex] = 0;
                                MessageBox.Show("Waiting time for P" + (target + 1) + " = " + wait_time_Oprocess[process_Oindex], "Waiting time:", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                burst_Oprocess[target] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (target = 0; target <= num_Oprocess - 1; target++)
                        {
                            if (priority_Oprocess[process_Oindex] == burst_Oprocess[target] && found == false)
                            {
                                wait_time_Oprocess[process_Oindex] = wait_time_Oprocess[process_Oindex - 1] + priority_Oprocess[process_Oindex - 1];
                                MessageBox.Show("Waiting time for P" + (target + 1) + " = " + wait_time_Oprocess[process_Oindex], "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                burst_Oprocess[target] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (process_Oindex = 0; process_Oindex <= num_Oprocess - 1; process_Oindex++)
                {
                    twt = twt + wait_time_Oprocess[process_Oindex];
                }
                MessageBox.Show("Average waiting time for " + num_Oprocess + " processes" + " = " + (awt = twt / num_Oprocess) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int num4process = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burst4process = new double[num4process];
                double[] wait4process = new double[num4process + 1];
                int[] priority4 = new int[num4process];
                int[] sort4priority = new int[num4process];
                int x, num;
                double twt = 0.0;
                double awt;
                int temp = 0;
                bool found = false;
                for (num = 0; num <= num4process - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    burst4process[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= num4process - 1; num++)
                {
                    string input2 =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    priority4[num] = Convert.ToInt16(input2);
                }
                for (num = 0; num <= num4process - 1; num++) // I can probably make copying this array ALOT easier 
                {
                    sort4priority[num] = priority4[num];
                }
                for (x = 0; x <= num4process - 2; x++) //I could also reduce this bulk with a simpler line of code   
                {
                    for (num = 0; num <= num4process - 2; num++)
                    {
                        if (sort4priority[num] > sort4priority[num + 1])
                        {
                            temp = sort4priority[num];
                            sort4priority[num] = sort4priority[num + 1];
                            sort4priority[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= num4process - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= num4process - 1; x++)
                        {
                            if (sort4priority[num] == priority4[x] && found == false)
                            {
                                wait4process[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wait4process[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                priority4[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= num4process - 1; x++)
                        {
                            if (sort4priority[num] == priority4[x] && found == false)
                            {
                                wait4process[num] = wait4process[num - 1] + burst4process[temp];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wait4process[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                priority4[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= num4process - 1; num++)
                {
                    twt = twt + wait4process[num];
                }
                MessageBox.Show("Average waiting time for " + num4process + " processes" + " = " + (awt = twt / num4process) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Console.WriteLine("\n\nAverage waiting time: " + (awt = twt / np));
                //Console.ReadLine();
            }
            else
            {
                //this.Hide();
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int nProcesses = Convert.ToInt16(userInput);
            int i, counter = 0;
            double total = 0.0;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTime = new double[10];
            double[] burstTime = new double[10];
            double[] temp = new double[10];
            int x = nProcesses;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < nProcesses; i++)
                {
                    string arrivalInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    arrivalTime[i] = Convert.ToInt64(arrivalInput);

                    string burstInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    burstTime[i] = Convert.ToInt64(burstInput);

                    temp[i] = burstTime[i];
                }
                string timeQuantumInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                               "",
                                                               -1, -1);

                timeQuantum = Convert.ToInt64(timeQuantumInput);
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total = total + temp[i];
                        temp[i] = 0;
                        counter = 1;
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] = temp[i] - timeQuantum;
                        total = total + timeQuantum;
                    }
                    if (temp[i] == 0 && counter == 1)
                    {
                        x--;
                        //printf("nProcess[%d]tt%dtt %dttt %d", i + 1, burst_time[i], total - arrival_time[i], total - arrival_time[i] - burst_time[i]);
                        MessageBox.Show("Turnaround time for Process " + (i + 1) + " : " + (total - arrivalTime[i]), "Turnaround time for Process " + (i + 1), MessageBoxButtons.OK);
                        MessageBox.Show("Wait time for Process " + (i + 1) + " : " + (total - arrivalTime[i] - burstTime[i]), "Wait time for Process " + (i + 1), MessageBoxButtons.OK);
                        turnaroundTime = (turnaroundTime + total - arrivalTime[i]);
                        waitTime = (waitTime + total - arrivalTime[i] - burstTime[i]);
                        counter = 0;
                    }
                    if (i == nProcesses - 1)
                    {
                        i = 0;
                    }
                    else if (arrivalTime[i + 1] <= total)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                }
                averageWaitTime = Convert.ToInt64(waitTime * 1.0 / nProcesses);
                averageTurnaroundTime = Convert.ToInt64(turnaroundTime * 1.0 / nProcesses);
                MessageBox.Show("Average wait time for " + nProcesses + " processes: " + averageWaitTime + " sec(s)", "", MessageBoxButtons.OK);
                MessageBox.Show("Average turnaround time for " + nProcesses + " processes: " + averageTurnaroundTime + " sec(s)", "", MessageBoxButtons.OK);
            }
        }
    }
}

