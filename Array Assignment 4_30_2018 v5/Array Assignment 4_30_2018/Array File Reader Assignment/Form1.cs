using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Array_File_Reader_Assignment
{
    public partial class Form1 : Form
    {

        
        //Create the iArray to hold all of he names 
        const int SIZE = 5000;
        string[] iArray = new string[SIZE];
        
        //Implements a TextReader that reads characters from a byte stream in a particular encoding.
        const string pathFinder = "Names.csv";
        StreamReader source = File.OpenText("Names.csv");

        //  Int variable for the counter
        int counter;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)

        {   //Gets a DateTime object that is set to the current date and time on this computer, expressed as the local time.
            DateTime start = DateTime.Now;
            DateTime Finish;
            TimeSpan Time;
            counter = 0;

            try
            {

                // Set the increment counter to 0
                for (counter = 0; counter < 5000; counter++)
                {
                    // Adds each line from stream reader source to each element in the array.
                    iArray[counter] = source.ReadLine();
                    // Displays the instance of i into the list box.
                    listBox1.Items.Add(iArray[counter]);
                }

                //Gets the stop time of the counter. 
                Finish = DateTime.Now;
                Time = Finish - start;

                //The length of time the procedure took to execute and displayed to listbox1
                label1.Text = listBox1.Items.Count.ToString() + " " + "Results Loaded in" + " " + (Time.TotalSeconds.ToString()) + " " + "seconds";

            }
            //Error handeling of code
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectionSort(string[] iArray)
        {
            try
            {
            //Variables for the following for loops.
            int minIndex;
            string minValue;

            for (int startScan = 0; startScan < iArray.Length - 1; startScan++)
            {
                minIndex = startScan;
                minValue = iArray[startScan];

                for (int index = startScan + 1; index < iArray.Length; index++)
                {
                    if (string.Compare(minValue, iArray[index], true) == 1)
                    {
                            ////minIndex functions is the smallest index, in a continuously-shrinking set of names, starting with
                            //the entire list, and lessening by 1 on each iteration. minValue is the value located at said index.
                            minValue = iArray[index];
                            minIndex = index;
                    }
                }
                //Calls the Swap Funcion and pass two paramaters into the method.
                Swap(ref iArray[minIndex], ref iArray[startScan]);

            }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong in the sorting process...");
            }
        }

        private void Swap(ref string a, ref string b)

        {
            string temp = a;
            a = b;
            b = temp;
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                listBox1.Items.Clear();

                DateTime start = DateTime.Now;
                DateTime Finish;
                TimeSpan Time;

                while (counter < iArray.Length && !source.EndOfStream)
                {
                    iArray[counter] = source.ReadLine();
                    counter++;
                }

                listBox1.Items.Clear();
                SelectionSort(iArray);

                foreach (string value in iArray)
                {
                    listBox1.Items.Add(value);
                }

                Finish = DateTime.Now;
                Time = Finish - start;

                label2.Text = listBox1.Items.Count.ToString()
                    + " " + "Results Sorted in" + " " + (Time.TotalSeconds.ToString()) + " " + "seconds";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

            
            string pathFinder2 = "sortedNames.csv";
            // Stream Writer to write the text files
            StreamWriter streamWriter = new StreamWriter(pathFinder2);
            // Writes the sorted values into the new file     
            foreach (string name in listBox1.Items)
            {
                streamWriter.WriteLine(name.ToString());
            }
                MessageBox.Show("File as been exported...");
            }
            catch (Exception)
            {
                MessageBox.Show("Error exporting file");      
            }

      
        }
        private int BinarySearch(string[] iArray, string value)
        {
            int first = 0;                      // first array element
            int last = iArray.Length - 1;       // last array element
            int middle;                         // Midpoint of search
            int position = -1;                  // position of search value
            bool found = false;                 // Flag

            // Search for the value
            while (!found && first <= last)
            {
                // Calc the midpoint.
                middle = (first + last) / 2;

                // If value is found at midpoint ...
                if (iArray[middle] == value)
                {
                    found = true;
                    position = middle;
                }

                // else if value is in lower half...
                else if (string.Compare(iArray[middle], value, false) > 0)
                {
                    last = middle - 1;
                }

                // else if value is in upper half...
                else
                {
                    first = middle + 1;
                }
            }
            // Return the position of the item, or -1
            // If it was not found.
            return position;
            
        }
        
        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Time variables
                DateTime start = DateTime.Now;
                DateTime Finish;
                TimeSpan Time;

                // name = text in textbox
                string Name = searchBox1.Text;
                // sets the position
                int position = BinarySearch(iArray, searchBox1.Text);
                // boolean determining if name is in the array
                bool Found = iArray.Contains(Name);
                // If it was not found, display this message

                if (position >= 0)
                {
                    Found = true;
                }

                if (Found == false)
                {
                    label5.Text = "This name could not be found";
                }
                // If found, do this
                
                else
                {
                    // Time stuff
                    Finish = DateTime.Now;
                    Time = Finish - start;
                    // Highlights the name

                    MessageBox.Show("This name was found at index # " + position);
                    listBox1.SetSelected(position, true);
                    string text = listBox1.GetItemText(listBox1.SelectedItem);
                    label5.Text = text + " " + "found in" + " " + (Time.TotalSeconds.ToString()) + " " + "seconds";
                }
            }
            catch
            {  // Sam did it
                MessageBox.Show("You broke it");
            }
        }
    }
}


        


  
    
