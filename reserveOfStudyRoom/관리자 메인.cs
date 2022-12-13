using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reserveOfStudyRoom
{

    public partial class 관리자_메인 : Form
    {
        private int SelectedRowIndex;
        OracleDataAdapter DBAdapter;
        DataSet DS;
        OracleCommandBuilder myCommandBuilder;
        DataTable reserveTable;


        private void DB_Open()
        {
            try
            {
                string connectionString = "User Id=moonu; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
                string commandString = "select * from r_room_res";
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                myCommandBuilder = new OracleCommandBuilder(DBAdapter);
                DS = new DataSet();
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }

        }


        public 관리자_메인()
        {
            InitializeComponent();
            DB_Open();
        }


        private void DBGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataSet DS = new DataSet();

                DBAdapter.Fill(DS, "r_room_res");

                DataTable reserveTable = DS.Tables["r_room_res"];
                if (e.RowIndex < 0)
                {
                    return;
                }
                else if (e.RowIndex > reserveTable.Rows.Count - 1)
                {
                    MessageBox.Show("해당하는 데이터가 존재하지 않습니다.");
                    return;
                }

                DataRow currRow = reserveTable.Rows[e.RowIndex];

                SelectedRowIndex = Convert.ToInt32(currRow["room_res_no"]);
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DS.Clear();

                // phone 테이블의 결과값을 DataSet 객체에 저장
                DBAdapter.Fill(DS, "r_room_res");

                // DataSet 중 phone 테이블에 해당하는 정보를 DBGrid 에 보여줌
                DBGrid.DataSource = DS.Tables["r_room_res"].DefaultView;
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        private void 관리자_메인_Load(object sender, EventArgs e)
        {
            this.DBGrid.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void NameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DS.Clear();
            DBAdapter.Fill(DS, "r_room_res");
            reserveTable = DS.Tables["r_room_res"];

            DataRow[] ResultRows = reserveTable.Select("name like '%" + TxtSearch.Text + "%'");

            DataColumn[] PrimaryKey = new DataColumn[1];
            PrimaryKey[0] = reserveTable.Columns["room_res_no"];
            reserveTable.PrimaryKey = PrimaryKey;

            DataRow currRow = reserveTable.Rows.Find(NameList.Text.Substring(0, 2));//*

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DS.Clear();

            DBAdapter.Fill(DS, "r_room_res");

            reserveTable = DS.Tables["r_room_res"];

            DataRow[] ResultRows = reserveTable.Select("stuName like '%" + TxtSearch.Text + "%'");

            NameList.Items.Clear();

            foreach (DataRow currRow in ResultRows)
            {
                NameList.Items.Add(currRow["stuID"].ToString() + " " + currRow["stuName"].ToString());
            }
        }



        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DS.Clear();
                DBAdapter.Fill(DS, "r_room_res");

                reserveTable = DS.Tables["r_room_res"];
                DataColumn[] PrimaryKey = new DataColumn[1];
                PrimaryKey[0] = reserveTable.Columns["room_res_no"];
                reserveTable.PrimaryKey = PrimaryKey;

                DataRow currRow = reserveTable.Rows.Find(SelectedRowIndex);
                // 해당 행 제거
                currRow.Delete();

                // 지운 후에 DataRowState 상태에서도 제거하고 변경사항 업데이트
                DBAdapter.Update(DS.GetChanges(DataRowState.Deleted), "r_room_res");

                DBGrid.DataSource = DS.Tables["r_room_res"].DefaultView;
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
    }
}
