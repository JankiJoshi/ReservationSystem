/*  AirlineReservationForm.cs
 *  Assignment 1
 *  Revision History
 *      Janki Joshi, 2016.09.21:   desgined
 *      Janki Joshi, 2016.09.22:   created
 *      Janki Joshi, 2016.09.25:   created [continued...]
 *      Janki Joshi, 2016.09.26:   added comments
 *      Janki Joshi, 2016.09.26:   debugged
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace JJoshiAssignment1
{
    /// <summary>
    /// Airline Reservation class to book, cancel and update status of airline seats.
    /// </summary>
    public partial class AirlineReservationForm : Form
    {
        //prints seat numbers in "[rownumber,seatnumber]" format
        int[,] seats = new int[5, 3];

        //stores passenger's name with particular seat position For e.g. arrayToBookSeat[0,2] = "Janki"
        string[,] arrayToBookSeat = new string[5, 3];

        //add 10 passenger's name in waiting list
        string[] waitingList = new string[10];

        //stores status of each seat.      
        string[,] status = new string[5, 3];

        //maintain the index of waitingList array.
        int k;

        /// <summary>
        /// Constructor ofthe form that initialize status array with "Available" status.
        /// </summary>
        public AirlineReservationForm()
        {
            int rowsLength = status.GetLength(0);
            int columnsLength = status.GetLength(1);
            try
            {
                for (int i = 0; i < rowsLength; i++)
                {
                    for (int j = 0; j < columnsLength; j++)
                    {
                        status[i, j] = "Available";
                    }
                }
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
            InitializeComponent();
        }

        /// <summary>
        /// This method books seat, validate passenger's identity and stores seat status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBook_Click(object sender, EventArgs e)
        {
            string passengerName = txtName.Text;
            int row = lstRows.SelectedIndex;
            int column = lstColumns.SelectedIndex;
            int rowsLength = seats.GetLength(0);
            int columnsLength = seats.GetLength(1);
            int i, j;
            int flag = 0;
            int flag2 = 0;

            try
            {
                if (passengerName == "" || row < 0 || column < 0)
                {
                    if (passengerName == "")
                    {
                        MessageBox.Show("Please enter your name");
                    }
                    if (row < 0 || column < 0)
                    {
                        MessageBox.Show("Row or Seat not selected");
                    }
                }
                else if (passengerName != "")
                {
                    for (int x = 0; x < passengerName.Length; x++)
                    {
                        if (char.IsNumber(passengerName[x]))
                        {
                            flag2 = 1;
                            break;
                        }
                    }
                    if (flag2 == 1)
                    {
                        MessageBox.Show("Please enter only alphabets");
                    }
                    else
                    {
                        passengerName = passengerName.Trim();
                        if (arrayToBookSeat[row, column] == null)
                        {
                            arrayToBookSeat[row, column] = passengerName;
                            status[row, column] = "Not Available";
                            MessageBox.Show("The seat [" + row + ", " + column + "] is booked");
                        }
                        else
                        {

                            for (i = 0; i < rowsLength; i++)
                            {
                                for (j = 0; j < columnsLength; j++)
                                {
                                    if (arrayToBookSeat[i, j] == null)
                                    {
                                        flag = 1;
                                        break;
                                    }
                                }
                            }
                            if (flag == 1)
                            {
                                MessageBox.Show("The seat [" + row + ", " + column + "] is booked. Choose Another");
                            }
                            else
                            {
                                for (k = 0; k < 10; k++)
                                {
                                    if (waitingList[k] == null)
                                    {
                                        waitingList[k] = passengerName;

                                        break;
                                    }
                                }
                                if (k < 9)
                                {
                                    MessageBox.Show("You are successfully added to waiting list");
                                }
                                else
                                {
                                    MessageBox.Show("All seats are full!");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
        }

        /// <summary>
        /// This method displays all booked and empty seats with passenger name 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnShowAll_Click(object sender, EventArgs e)
        {
            richTxtBookedPassengers.Clear();
            int rowsLength = seats.GetLength(0);
            int columnsLength = seats.GetLength(1);
            int i, j;

            try
            {
                for (i = 0; i < rowsLength; i++)
                {
                    for (j = 0; j < columnsLength; j++)
                    {
                        richTxtBookedPassengers.Text += "[" + i + ", " + j + "]-" + arrayToBookSeat[i, j] + "\n";
                    }
                }
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
        }

        /// <summary>
        /// This method cancel selected seat and make its status available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstRows.SelectedIndex < 0 || lstColumns.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select the row and column to cancel the seat");
                }
                else
                {
                    if (arrayToBookSeat[lstRows.SelectedIndex, lstColumns.SelectedIndex] == null)
                    {
                        MessageBox.Show("Seat is already Empty ! You should have to 'BOOK' it first");
                    }
                    else
                    {
                        arrayToBookSeat[lstRows.SelectedIndex, lstColumns.SelectedIndex] = null;
                        status[lstRows.SelectedIndex, lstColumns.SelectedIndex] = "Available";
                        MessageBox.Show("The seat is cancelled");

                        for (k = 0; k < 10; k++)
                        {
                            if (waitingList[k] != null)
                            {
                                arrayToBookSeat[lstRows.SelectedIndex, lstColumns.SelectedIndex] = waitingList[k];
                                waitingList[k] = null;
                                for (int i = 0; i < 10; i++)
                                {
                                    waitingList[i] = waitingList[i + 1];
                                }                                
                                status[lstRows.SelectedIndex, lstColumns.SelectedIndex] = "Not Available";
                                MessageBox.Show("Moved the 1st person from waiting list to cancelled seat");
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
        }

        /// <summary>
        /// This method books all seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFillAll_Click(object sender, EventArgs e)
        {
            richTxtBookedPassengers.Clear();
            int rowsLength = seats.GetLength(0);
            int columnsLength = seats.GetLength(1);
            int i, j;
            try
            {
                for (i = 0; i < rowsLength; i++)
                {
                    for (j = 0; j < columnsLength; j++)
                    {
                        arrayToBookSeat[i, j] = "Janki";
                        richTxtBookedPassengers.Text += "[" + i + ", " + j + "]-" + arrayToBookSeat[i, j] + "\n";
                        status[i, j] = "Not Available";
                    }
                }
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
        }

        /// <summary>
        /// This method adds passenger name to waiting list in FIFO manner list when all seats are reserved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToWaitingList_Click(object sender, EventArgs e)
        {
            string passengerName = txtName.Text;
            int rowsLength = seats.GetLength(0);
            int columnsLength = seats.GetLength(1);
            int row = lstRows.SelectedIndex;
            int column = lstColumns.SelectedIndex;
            int flag1 = 0;

            try
            {
                if (passengerName == "" || row < 0 || column < 0)
                {
                    if (passengerName == "")
                    {
                        MessageBox.Show("Please enter your name");
                    }
                    if (row < 0 || column < 0)
                    {
                        MessageBox.Show("Row or Seat not selected");
                    }
                }
                else
                {
                    for (int i = 0; i < rowsLength; i++)
                    {
                        for (int j = 0; j < columnsLength; j++)
                        {
                            if (arrayToBookSeat[i, j] == null)
                            {
                                flag1 = 1;
                            }
                        }
                    }
                    if (flag1 == 1)
                    {
                        MessageBox.Show("There are still 'Unreserved seats' are available !");
                    }
                    else
                    {
                        for (k = 0; k < 10; k++)
                        {
                            if (waitingList[k] == null)
                            {
                                waitingList[k] = passengerName;
                                break;
                            }
                        }
                        if (k < 9)
                        {
                            MessageBox.Show("You are successfully added to waiting list");
                        }
                        else
                        {
                            MessageBox.Show("All seats are full!");
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
        }

        /// <summary>
        /// This method diplays all the passenger name that are in waiting list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowWaitingList_Click(object sender, EventArgs e)
        {
            richTxtWaitingPassengers.Clear();
            int i;
            try
            {                
                for (i = 0; i < 10; i++)
                {                    
                    if (waitingList[i] != null)
                    {
                        richTxtWaitingPassengers.Text += "[" + i + "]-" + waitingList[i] + "\n";
                    }
                    else
                    {
                        richTxtWaitingPassengers.Text += "[" + i + "]-" + waitingList[i] + "\n";
                    }

                }
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
        }

        /// <summary>
        /// Displays status of seats of selected seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatus_Click(object sender, EventArgs e)
        {
            int row = lstRows.SelectedIndex;
            int column = lstColumns.SelectedIndex;            
            try
            {
                if (row < 0 || column < 0)
                {
                    MessageBox.Show("Please select row and column");
                }
                else
                {
                    txtStatus.Text = status[row, column].ToString();
                }                
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
        }
    }
}





