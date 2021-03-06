﻿using CygSoft.SmartSession.Infrastructure;
using CygSoft.SmartSession.Repositories.Implementation;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class SeedRepository : SQLiteContext
    {
        public void Goal()
        {
            var sql = @"
                INSERT INTO goal (title) VALUES 
                ('Happy Chappy'),
                ('Britannia Hotel'),
                ('Hollywood Bets');
            ";
            ExecuteNonQuery(sql);
        }

        public void Task()
        {
            var sql = @"
                INSERT INTO task (title, type) VALUES 
                ('Happy Chappy', 'P'),
                ('Britannia Hotel', 'M'),
                ('Hollywood Bets', 'D');
            ";
            ExecuteNonQuery(sql);
        }

        public void User()
        {
            var _password = new PasswordHash().Go("password1"); //7C6A180B36896A0A8C02787EEAFB0E4C

            var sql = string.Format(@"
                INSERT INTO user (has_access, cellphone, password, name, surname) VALUES 
                (1, 0820000000, '{0}', 'Joe', 'Blogs'),
                (1, 0830000000, '{0}', 'Sue', 'Blogs');",
                _password);
            ExecuteNonQuery(sql);
        }
    }
}
