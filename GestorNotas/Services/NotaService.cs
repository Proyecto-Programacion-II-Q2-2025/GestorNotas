﻿


using GestorNotas.Models;
using SQLite;

namespace GestorNotas.Services
{
    public class NotasServices
    {
        private readonly SQLiteConnection _connection;

        public NotasServices()
        {
            // Path to storage database
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notas.db3");
            // Initialize connection and create database
            _connection = new SQLiteConnection(dbPath);
            // Create table
            _connection.CreateTable<Notas>();

        }

        public List<Notas> GetAll()
        {
            // It's equal to SELECT * FROM Empleado
            return _connection.Table<Notas>().ToList();
        }

        public int Insert(Notas notas)
        {
            // It's equal to INSERT Empleado VALUES(....
            return _connection.Insert(notas);
        }

        public int Update(Notas notas)
        {
            // It's equal to UPDATE Empleado set xxxxx=xxx WHERE id = xxx
            return _connection.Update(notas);
        }

        public int Delete(Notas notas)
        {
            // It's equal to DELETE FROM Empleado WHERE id = xxx
            return _connection.Delete(notas);
        }
    }
}