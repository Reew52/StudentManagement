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

        private string selectedStudentId; // Biến để lưu trữ ID của sinh viên đang được chọn

        private void dgvStudent_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudent.SelectedRows.Count > 0)
            {
                // Lấy ID của sinh viên từ dòng đang được chọn trong DataGridView
                selectedStudentId = dgvStudent.SelectedRows[0].Cells[0].Value.ToString();

                // Tìm sinh viên trong danh sách dựa trên ID
                Student selectedStudent = students.FirstOrDefault(student => student.Id.Equals(selectedStudentId));

                if (selectedStudent != null)
                {
                    // Hiển thị thông tin sinh viên trong các TextBox
                    txtId.Text = selectedStudent.Id;
                    txtName.Text = selectedStudent.Name;
                    txtAge.Text = selectedStudent.Age.ToString();

                    if (selectedStudent.Gender == "Male")
                    {
                        rbMale.Checked = true;
                    }
                    else if (selectedStudent.Gender == "Female")
                    {
                        rbFemale.Checked = true;
                    }

                    txtPhoneNumber.Text = selectedStudent.PhoneNumber.ToString();
                    txtEmail.Text = selectedStudent.Email;
                }
            }
        }

        public studentManagementForm()
        {
            InitializeComponent();

            // Thêm hai bản ghi mẫu vào mảng students
            students[0] = new Student("SV001", "Nguyen Van A", 19, "Male", 123456789, "nguyenvana@example.com");
            students[1] = new Student("SV002", "Le Thi B", 20, "Female", 987654321, "lethib@example.com");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string name = txtName.Text;
            int age;

            if (!int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Vui lòng nhập một giá trị số nguyên hợp lệ cho Age.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int phoneNumber;

            if (!int.TryParse(txtPhoneNumber.Text, out phoneNumber))
            {
                MessageBox.Show("Vui lòng nhập một giá trị số nguyên hợp lệ cho Phone Number.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string gender;

            if (rbMale.Checked)
            {
                gender = "Male";
            }
            else if (rbFemale.Checked)
            {
                gender = "Female";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một giới tính.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string email = txtEmail.Text;

            // Kiểm tra các trường dữ liệu có được điền đầy đủ không
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) ||
                phoneNumber == 0 || age == 0 || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo đối tượng Student mới
            Student newStudent = new Student(id, name, age, gender, phoneNumber, email);

            // Thêm sinh viên vào mảng students (hoặc cơ sở dữ liệu nếu bạn sử dụng)
            AddStudent(newStudent);

            // Hiển thị thông báo thành công
            MessageBox.Show("Thêm sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Xóa các trường dữ liệu sau khi thêm
            txtId.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";

            // Refresh DataGridView
            btRefresh_Click(sender, e);
        }

        private void AddStudent(Student student)
        {
            // Tìm vị trí trống trong mảng students để thêm sinh viên
            for (int i = 0; i < students.Length; i++)
            {
                if (students[i] == null)
                {
                    students[i] = student;
                    break; // Thêm thành công, thoát khỏi vòng lặp
                }
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem đã chọn một dòng để chỉnh sửa chưa
            if (string.IsNullOrWhiteSpace(selectedStudentId))
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để chỉnh sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tìm sinh viên trong danh sách dựa trên ID
            Student studentToUpdate = students.FirstOrDefault(student => student.Id.Equals(selectedStudentId));

            if (studentToUpdate == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên cần chỉnh sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật thông tin sinh viên với các giá trị mới từ các TextBox
            studentToUpdate.Name = txtName.Text;
            studentToUpdate.Age = int.Parse(txtAge.Text);

            if (rbMale.Checked)
            {
                studentToUpdate.Gender = "Male";
            }
            else if (rbFemale.Checked)
            {
                studentToUpdate.Gender = "Female";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một giới tính.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            studentToUpdate.PhoneNumber = int.Parse(txtPhoneNumber.Text);
            studentToUpdate.Email = txtEmail.Text;

            // Hiển thị thông báo thành công
            MessageBox.Show("Cập nhật thông tin sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh DataGridView
            btRefresh_Click(sender, e);

            // Xóa các trường dữ liệu sau khi cập nhật
            txtId.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";

            // Reset selectedStudentId
            selectedStudentId = "";

        }

        private void btRefreshInput_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn một dòng để xóa chưa
            if (string.IsNullOrWhiteSpace(selectedStudentId))
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo một mảng mới có kích thước nhỏ hơn để xóa sinh viên
            Student[] updatedStudents = new Student[students.Length - 1];
            int newIndex = 0;

            for (int i = 0; i < students.Length; i++)
            {
                if (students[i] != null && !students[i].Id.Equals(selectedStudentId))
                {
                    updatedStudents[newIndex] = students[i];
                    newIndex++;
                }
            }

            // Hiển thị thông báo xóa thành công
            MessageBox.Show("Xóa sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh DataGridView
            btRefresh_Click(sender, e);

            // Xóa các trường dữ liệu sau khi xóa
            txtId.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";

            // Reset selectedStudentId
            selectedStudentId = "";

            // Cập nhật lại danh sách sinh viên
            students = updatedStudents;

            btRefresh_Click(sender, e);
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Student[] searchResults = new Student[students.Length];
            int newIndex = 0;

            foreach (Student student in students)
            {
                if (student != null && (student.Id.Contains(searchText) || student.Name.Contains(searchText) ||
                    student.Gender.Contains(searchText) || student.PhoneNumber.ToString().Contains(searchText) ||
                    student.Email.Contains(searchText)))
                {
                    searchResults[newIndex] = student;
                    newIndex++;
                }
            }

            // Create a new array with the actual size of the search results
            Student[] finalSearchResults = new Student[newIndex];
            Array.Copy(searchResults, finalSearchResults, newIndex);

            // Display the search results in DataGridView
            DisplayStudentsInDataGridView(finalSearchResults);
        }

        private void DisplayStudentsInDataGridView(Student[] students)
        {
            // Xóa tất cả các dòng hiện có trong DataGridView
            dgvStudent.Rows.Clear();

            foreach (Student student in students)
            {
                // Thêm một dòng mới vào DataGridView với thông tin của sinh viên
                dgvStudent.Rows.Add(student.Id, student.Name, student.Age, student.Gender, student.PhoneNumber, student.Email);
            }
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

        private void ádas(object sender, EventArgs e)
        {

        }
    }
}
