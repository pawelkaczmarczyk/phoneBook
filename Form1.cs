using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'appData.PhoneBooks' . Możesz go przenieść lub usunąć.
            this.phoneBooksTableAdapter.Fill(this.appData.PhoneBooks);
            Edit(false);

        }

        private void Edit(bool value)
        {
            txtPhone.Enabled = value;
            txtName.Enabled = value;
            txtMail.Enabled = value;
            txtAdress.Enabled = value;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Edit(true);
                appData.PhoneBooks.AddPhoneBooksRow(appData.PhoneBooks.NewPhoneBooksRow());
                phoneBooksBindingSource.MoveLast();
                txtPhone.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                appData.PhoneBooks.RejectChanges();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit(true);
            txtPhone.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Edit(false);
            phoneBooksBindingSource.ResetBindings(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Edit(false);
                phoneBooksBindingSource.EndEdit();
                phoneBooksTableAdapter.Update(appData.PhoneBooks);
                dataGridView1.Refresh();
                txtPhone.Focus();
                MessageBox.Show("Your data has been succesfully saved", "Message",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                appData.PhoneBooks.RejectChanges();
            }

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    phoneBooksBindingSource.Filter = string.Format("PhoneNumber = '{0}' OR FullName LIKE '*{1}*' OR Mail = '{2}' OR Adress LIKE '*{3}*'", txtPhone, txtName, txtMail, txtAdress);
                }
                else
                {
                    phoneBooksBindingSource.Filter = string.Empty;
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Delete)
            {
                if(MessageBox.Show("You","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.OK)
                {
                    phoneBooksBindingSource.RemoveCurrent();
                }
            }
        }
    }
}
