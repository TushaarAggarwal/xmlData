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
using System.Xml;

namespace xmlData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetData();
            RichtextData();
        }

        public void RichtextData()
        {
            DataSet dsResult = new DataSet();
            dsResult.ReadXml("employee.xml");
            if (dsResult.Tables.Count != 0)
            {

                DataTable dt = dsResult.Tables[0];



                foreach (DataRow row in dt.Rows)
                {
                    this.richTextBox1.Text += row[0] + " " + row[1] + " " + row[2].ToString() + "\n";

                }
            }
        }



        public void GetData()
        {
            try
            {
                DataSet dsResult = new DataSet();
                dsResult.ReadXml("employee.xml");
                if (dsResult.Tables.Count != 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dsResult.Tables[0];

                       


                    }
                }



               
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {



            /*Save*/
            using (DataSet dsResult = new DataSet())
            {
                dsResult.ReadXml("employee.xml");
                if (dsResult.Tables.Count == 0)
                {
                    EmployeeModel objStudentModel = new EmployeeModel();
                    objStudentModel.empid= Convert.ToInt32(txtempid.Text);
                    objStudentModel.empnm = txtnm.Text;
                    objStudentModel.age = Convert.ToInt32(txtage.Text);
                    objStudentModel.desg = txtdesig.Text;

                    XmlTextWriter writer = new XmlTextWriter("employee.xml", System.Text.Encoding.UTF8);

                    writer.WriteStartDocument(true);
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    writer.WriteStartElement("Employee");

                  //  writer.WriteStartElement("Employee");

                    writer.WriteStartElement("empid");
                    writer.WriteString(objStudentModel.empid.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("name");
                    writer.WriteString(objStudentModel.empnm);
                    writer.WriteEndElement();

                    writer.WriteStartElement("age");
                    writer.WriteString(objStudentModel.age.ToString());
                    writer.WriteEndElement();


                    writer.WriteStartElement("desig");
                    writer.WriteString(objStudentModel.desg.ToString());
                    writer.WriteEndElement();



                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                    dsResult.ReadXml("employee.xml");
                }
                else
                {
                    //-- Add one new row in to dataset and set the column data accordingly.
                    dsResult.Tables[0].Rows.Add(dsResult.Tables[0].NewRow());
                    dsResult.Tables[0].Rows[dsResult.Tables[0].Rows.Count - 1]["empid"] = txtempid.Text;
                    dsResult.Tables[0].Rows[dsResult.Tables[0].Rows.Count - 1]["name"] = txtnm.Text.ToUpper();
                    dsResult.Tables[0].Rows[dsResult.Tables[0].Rows.Count - 1]["age"] = txtage.Text;
                    dsResult.Tables[0].Rows[dsResult.Tables[0].Rows.Count - 1]["desig"] = txtdesig.Text;
                    dsResult.AcceptChanges();
                    //-- Write final data to XML file using write method
                    dsResult.WriteXml("employee.xml", XmlWriteMode.IgnoreSchema);
                }
                dataGridView1.DataSource = dsResult.Tables[0];
              


                MessageBox.Show("Data Saved Successfully.");
            }
        


    }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1 != null)
            {
                richTextBox1.Text = "";
                RichtextData();
            }
            else
            {
                RichtextData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = "emp.xml";
            XmlDocument doc = new XmlDocument();

            //If there is no current file, then create a new one

            if (!System.IO.File.Exists(path))
            {
                //Create neccessary nodes
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                XmlComment comment = doc.CreateComment("This is an XML Generated File");
                
                doc.AppendChild(declaration);
                doc.AppendChild(comment);
                


            }
            else //If there is already a file
            {
                //    //Load the XML File
                doc.Load(path);
            }

            //Get the root element
            XmlElement root = doc.DocumentElement;

            XmlElement Subroot = doc.CreateElement("emp");
            XmlElement empid1 = doc.CreateElement("empid");
            XmlElement name2 = doc.CreateElement("name");
            XmlElement age3 = doc.CreateElement("age");
            XmlElement desig4 = doc.CreateElement("desig");


            //Add the values for each nodes


            empid1.InnerText = txtempid.Text;
            name2.InnerText = txtnm.Text;
            age3.InnerText = txtage.Text;
            desig4.InnerText = txtdesig.Text;



            Subroot.AppendChild(empid1);
            Subroot.AppendChild(name2);
            Subroot.AppendChild(age3);
            Subroot.AppendChild(desig4);

            
            root.AppendChild(Subroot);
            doc.AppendChild(root);

            //Save the document
            doc.Save(path);


            //Show confirmation message
            MessageBox.Show("Details  added Successfully");
        }
    }
}
