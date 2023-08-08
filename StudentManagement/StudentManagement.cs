using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class studentManagementForm : Form
    {
        private Student[] students = new Student[100]; // Khai báo biến students trong lớp
        public studentManagementForm()
        {
            InitializeComponent();

            // Thêm hai bản ghi mẫu vào mảng students
            students[0] = new Student("SV001", "Nguyen Van A", 19, "Male", "123456789", "nguyenvana@example.com");
            students[1] = new Student("SV002", "Le Thi B", 20, "Female", "987654321", "lethib@example.com");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btAdd_Click(object sender, EventArgs e)
        {

        }

        private void btEdit_Click(object sender, EventArgs e)
        {

        }

        private void btRefreshInput_Click(object sender, EventArgs e)
        {

        }

        private void btDelete_Click(object sender, EventArgs e)
        {

        }

        private void btSearch_Click(object sender, EventArgs e)
        {

        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            // Xóa hết dữ liệu hiện có trong DataGridView
            dgvStudent.Rows.Clear();

            // Lặp qua mảng students và thêm dữ liệu vào DataGridView
            foreach (Student student in students)
            {
                if (student != null)
                {
                    dgvStudent.Rows.Add(student.Id, student.Name, student.Age, student.Gender, student.PhoneNumber, student.Email);
                }
            }
        }

        private void btLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Đóng form quản lý sinh viên
                this.Close();

                // Hiển thị lại form đăng nhập
                loginForm loginForm = new loginForm();
                loginForm.Show();
            }
        }
    }
}
