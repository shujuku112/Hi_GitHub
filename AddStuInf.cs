Hi_GitHub
=========

  //隐藏相应的textBox控件
        void Hide()
        {
            lbStuNo.Visible = false;
            lbStuName.Visible = false;
            lbStuSex.Visible = false;
            lbDor.Visible = false;
            lbStuBed.Visible = false;
            lbStuGrade.Visible = false;
            txtStuNo.Visible = false;
            txtStuName.Visible = false;
            txtStuSex.Visible = false;
            txtDorNo.Visible = false;
            txtBedNo.Visible = false;
            txtStuGrade.Visible = false;
            btnSure.Visible = false;
            btnEdit.Visible = false;
            btn_EditSure.Visible = false;
        }

        //将隐藏的控件显示出来.
        void Display()
        {
            lbStuNo.Visible = true;
            lbStuName.Visible = true;
            lbStuSex.Visible = true;
            lbDor.Visible = true;
            lbStuBed.Visible = true;
            lbStuGrade.Visible = true;
            txtStuNo.Visible = true;
            txtStuName.Visible = true;
            txtStuSex.Visible = true;
            txtDorNo.Visible = true;
            txtBedNo.Visible = true;
            txtStuGrade.Visible = true;
        }

        //清空textBox控件
        void ClearAll()
        {
            txtBedNo.Text = "";
            txtDorNo.Text = "";
            txtStuGrade.Text = "";
            txtStuName.Text = "";
            txtStuNo.Text = "";
        }


        //读取数据库中的数据显示在dataGridView中
        void RefreshData()
        {
            string sqlStr;
            sqlStr = "select * from T_Student";
            DataSet myds = new DataSet();
            myds = DataBase.GetDataFromDB(sqlStr);
            if (myds != null)
            {
                dgStuDorInf.DataSource = myds.Tables[0];
                dgStuDorInf.Columns[0].HeaderText = "学号";
                dgStuDorInf.Columns[1].HeaderText = "姓名";
                dgStuDorInf.Columns[2].HeaderText = "性别";
                dgStuDorInf.Columns[3].HeaderText = "寝室号";
                dgStuDorInf.Columns[4].HeaderText = "床号";
                dgStuDorInf.Columns[5].HeaderText = "班级";
                dgStuDorInf_RowHeaderMouseClick(null, null);

            }
            else
            {
                dgStuDorInf.DataSource = null;
            }
        }


        //判断信息是否重复
        bool No(string no)
        {
            int n = dgStuDorInf.Rows.Count;
            for (int i = 0; i < n - 1; i++)
            {
                if (no == dgStuDorInf.Rows[i].Cells[0].Value.ToString().Trim())
                    return false;
            }
            return true;
        }


        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Hide();//加载时隐藏相应的控件.
        }

        private void dgStuDorInf_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Display();
                int n = dgStuDorInf.CurrentCell.RowIndex;
                if (n < dgStuDorInf.RowCount)
                {
                    txtStuName.Text = dgStuDorInf[0, n].Value.ToString().Trim();
                    txtStuNo.Text = dgStuDorInf[1, n].Value.ToString().Trim();
                    txtStuSex.Text = dgStuDorInf[2, n].Value.ToString().Trim();
                    txtDorNo.Text = dgStuDorInf[3, n].Value.ToString().Trim();
                    txtBedNo.Text = dgStuDorInf[4, n].Value.ToString().Trim();
                    txtStuGrade.Text = dgStuDorInf[5, n].Value.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


   private void btnSure_Click(object sender, EventArgs e)//添加学生入住信息
        {
            try
            {
                if (txtStuNo.Text.Trim() == "")
                {
                    txtStuNo.Focus();
                    return;
                }
                else if (txtStuName.Text.Trim() == "")
                {
                    MessageBox.Show("姓名不能为空!", "提示");
                    txtStuName.Focus();
                    return;
                }
                else if (txtStuSex.Text.Trim() == "")
                {
                    MessageBox.Show("性别不能为空!", "提示");
                    txtStuSex.Focus();
                    return;
                }
                else if (txtBedNo.Text.Trim() == "")
                {
                    MessageBox.Show("床号不能为空!", "提示");
                    txtBedNo.Focus();
                    return;
                }
                else if (txtStuGrade.Text.Trim() == "")
                {
                    MessageBox.Show("班级不能为空!", "提示");
                    txtStuGrade.Focus();
                    return;
                }
                string sqlStr;
                sqlStr = "insert into T_Student values('" + txtStuName.Text.Trim() + "','" + txtStuNo.Text.Trim() + "','" + txtStuSex.Text.Trim() + "','" + txtDorNo.Text.Trim() + "','" + txtBedNo.Text.Trim() + "','" + txtStuGrade.Text.Trim() + "')";
                DataBase.Update(sqlStr);
                RefreshData();
                if (MessageBox.Show("添加成功!继续添加吗?", "添加信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                DataBase.myconn.Close();
                MessageBox.Show(ex.Message);
                ClearAll();
                dgStuDorInf.Enabled = true;
                dgStuDorInf_RowHeaderMouseClick(null, null);
            }

        }

        private void btnDisplay_Click(object sender, EventArgs e)//显示学生的入住信息
        {
            try
            {
                string sqlStr = "";
                sqlStr = "select * from T_Student ";
                DataSet ds = new DataSet();
                ds = DataBase.GetDataFromDB(sqlStr);
                if (ds != null)
                {
                    dgStuDorInf.DataSource = ds.Tables[0];
                    dgStuDorInf.Columns[0].HeaderText = "学号";
                    dgStuDorInf.Columns[1].HeaderText = "姓名";
                    dgStuDorInf.Columns[2].HeaderText = "性别";
                    dgStuDorInf.Columns[3].HeaderText = "寝室号";
                    dgStuDorInf.Columns[4].HeaderText = "床号";
                    dgStuDorInf.Columns[5].HeaderText = "班级";
                    dgStuDorInf_RowHeaderMouseClick(null, null);
                    ClearAll();
                }
                else
                {
                    dgStuDorInf.DataSource = null;
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                DataBase.myconn.Close();
                MessageBox.Show(ex.Message);
            }
            Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)//学生退房
        {
            try
            {
                if (MessageBox.Show("确定退房?", "退房", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    string sqlStr;
                    sqlStr = "delete from T_Student where Sno='" + txtStuName.Text.Trim() + "'";
                    DataBase.Update(sqlStr);
                    int n = dgStuDorInf.CurrentCell.RowIndex;
                    dgStuDorInf.Rows.RemoveAt(n);
                    if (dgStuDorInf.Rows.Count == 1)
                    {
                        ClearAll();
                        dgStuDorInf.DataSource = null;
                    }

                    else
                    {
                        dgStuDorInf_RowHeaderMouseClick(null, null);
                    }
                }

            }
            catch (Exception ex)
            {
                DataBase.myconn.Close();
                MessageBox.Show(ex.Message);
            }
        }
