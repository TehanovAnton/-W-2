﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicsParService
{
    class Work : SqlCrud
    {
        public Work(Db db)
        {
            _db = db;
        }

        public override void GetAll()
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = "select * from works",
                Connection = _db.Connection
            };

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                string columnName1 = reader.GetName(0);
                string columnName2 = reader.GetName(1);

                Console.WriteLine($"{columnName1}\t {columnName2}");

                while (reader.Read())
                {
                    object id = reader.GetValue(0);
                    object name = reader.GetValue(1);

                    Console.WriteLine($"\t{id}: \t{name} ...");
                }
            }

            reader.Close();
            _db.closeConnection();

            Console.WriteLine($"------------------------------------");
        }

        public bool Insert(string name)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"insert into works(id, name)" +
                               $"values ({LastId() + 1}, '{name}')",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_inserted = changedRows == 1;
            string message = changedRows == 1 ? "Работа добавлена" : "Работа не добавлена";
            Console.WriteLine(message);
            return is_inserted;
        }

        public bool Update(int id, string name)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"update works set name = '{name}' where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_updated = changedRows == 1;
            string message = is_updated ? "Работа обновлён" : "Работа не обновлён";
            Console.WriteLine(message);
            return is_updated;
        }

        public override bool Delete(int id)
        {
            _db.openConnection();

            SqlCommand command = new SqlCommand
            {
                CommandText = $"delete from works where id = {id}",
                Connection = _db.Connection
            };

            int changedRows = command.ExecuteNonQuery();

            _db.closeConnection();

            bool is_deleted = changedRows == 1;
            string message = is_deleted ? "Работа удалёна" : "Работа не удалёна";
            Console.WriteLine(message);
            return is_deleted;
        }

        public override int LastId()
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "select top 1 * from works order by id desc",
                Connection = _db.Connection
            };

            int res = (int)command.ExecuteScalar();

            return res;
        }
    }
}
