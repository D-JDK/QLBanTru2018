﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataConnect.DAO.TienBao;
using DataConnect.DAO.HungTD;
using DataConnect.ViewModel;
using System.IO;
using System.Drawing.Imaging;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;

namespace QLHSBanTru2018_Demo_V1.TienBao
{
    public partial class frmHealthExaminationDetail : DevExpress.XtraEditors.XtraUserControl
    {
        #region System
        public frmHealthExaminationDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region LoadInfor
        private void LoadClassInfor(int GradeID)
        {
            List<DataConnect.Class> listClass = new ClassDAO().ListClassByGrade(GradeID);
            cmbLopHoc.DisplayMember = "Name";
            cmbLopHoc.ValueMember = "ClassID";
            cmbLopHoc.DataSource = listClass;
        }
        private void LoadGradeInfor(int SemesterID)
        {
            List<DataConnect.Grade> ListGrade = new DataConnect.DAO.HungTD.GradeDAO().ListBySemester(SemesterID);
            cmbKhoiLop.DisplayMember = "Name";
            cmbKhoiLop.ValueMember = "GradeID";
            cmbKhoiLop.DataSource = ListGrade;
        }
        private void LoadSemesterInfor(int CourseID)
        {
            List<DataConnect.Semester> ListSemester = new SemesterDAO().ListByCourseID(CourseID);
            cmbHocKy.DisplayMember = "Name";
            cmbHocKy.ValueMember = "SemesterID";
            cmbHocKy.DataSource = ListSemester;
        }
        private void LoadCourseInfor()
        {
            List<DataConnect.Course> ListCourse = new CourseDAO().ListAll();
            cmbNamHoc.DataSource = ListCourse;
            cmbNamHoc.DisplayMember = "Name";
            cmbNamHoc.ValueMember = "CourseID";
        }
        private void LoadHealthExaminationInfor()
        {            
            cmbHealthExam.DataSource = new HealthExaminationDetailDAO().ListHealthExamination(); ;
            cmbHealthExam.DisplayMember = "HealthExaminationName";
            cmbHealthExam.ValueMember = "HealthExaminationID";
        }
        private void FillGridControl(int ClassID, int HealthExaminationID)
        {
            try
            {
                dgvHealthDetail.DataSource = new HealthExaminationDetailDAO().ListHealthDetail(ClassID, HealthExaminationID);

            }
            catch
            { }
        }
        private void Danhsach(int ClassID)
        {
            try
            {
                dgvHealthDetail.DataSource = new HealthExaminationDetailDAO().ListStudent(ClassID);

            }
            catch
            { }
        }
        private void ListGridView(int ClassID, int HealthExaminationID)
        {
            try
            {
                dgvHealthDetail.DataSource = new HealthExaminationDetailDAO().ListGridView(ClassID, HealthExaminationID);

            }
            catch
            { }
        }


        #endregion

        #region Event

        private void frmHealthExaminationDetail_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            
        }
        private void cmbHealthExam_Click(object sender, EventArgs e)
        {
            LoadHealthExaminationInfor();
        }

        private void cmbNamHoc_Click(object sender, EventArgs e)
        {
            LoadCourseInfor();
        }
        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadSemesterInfor(int.Parse(cmbNamHoc.SelectedValue.ToString()));
            }
            catch
            { }
        }
        private void cmbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadGradeInfor(int.Parse(cmbHocKy.SelectedValue.ToString()));

            }
            catch
            { }
        }
        private void cmbKhoiHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadClassInfor(int.Parse(cmbKhoiLop.SelectedValue.ToString()));

            }
            catch
            { }
        }

        private void btnXemchitiet_Click(object sender, EventArgs e)
        {
            if (cmbHealthExam.SelectedValue == null )
            {
                XtraMessageBox.Show("Mời bạn chọn đợt khám sức khỏe", "Thông báo");
               
            }
            else if(cmbLopHoc.SelectedValue == null)
            {
                XtraMessageBox.Show("Mời bạn chọn lớp học", "Thông báo" );
            }
            else
            {
                FillGridControl(int.Parse(cmbLopHoc.SelectedValue.ToString()), int.Parse(cmbHealthExam.SelectedValue.ToString()));
            }
        }
        private void btnDanhsach_Click(object sender, EventArgs e)
        {
            if (cmbHealthExam.SelectedValue == null)
            {
                XtraMessageBox.Show("Mời bạn chọn đợt khám sức khỏe", "Thông báo");
                
            }
            else if (cmbLopHoc.SelectedValue == null)
            {
                XtraMessageBox.Show("Mời bạn chọn lớp học", "Thông báo");
            }
            else
            {
                frmNewHealthExamDetail m_frmNewHealth = new frmNewHealthExamDetail();
             
                m_frmNewHealth.iFunction = 1;
                m_frmNewHealth.healthExamination = new HealthExaminationDAO().GetByID(int.Parse(cmbHealthExam.SelectedValue.ToString()));
                m_frmNewHealth.Class = new ClassDAO().GetByClassID(int.Parse(cmbLopHoc.SelectedValue.ToString()));
                m_frmNewHealth.ShowDialog();
                if (m_frmNewHealth.DialogResult == DialogResult.OK)
                {
                    FillGridControl(int.Parse(cmbLopHoc.SelectedValue.ToString()), int.Parse(cmbHealthExam.SelectedValue.ToString()));
                }

               
            }
           
        }

        #endregion


    }
}
