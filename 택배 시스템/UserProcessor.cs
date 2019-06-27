using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 택배_시스템
{
    public class UserSchema
    {
        public int key;
        public ulong id;
        public string name;
        public string address;
        public string phone;
        public int permission;
        public UserSchema()
        {

        }
        public UserSchema( int key, ulong id, string name, string address, string phone, int permission)
        {
            this.key = key;
            this.id = id;
            this.name = name;
            this.address = address;
            this.phone = phone;
            this.permission = permission;
        }
        public static UserSchema ReadStringLine(string target, params char[] separator)
        {
            string[] tmp = target.Split(separator);
            if(tmp.Length != 6)
            {
                throw new Exception();
            }
            return new UserSchema(int.Parse(tmp[0]), ulong.Parse(tmp[1]), tmp[2], tmp[3], tmp[4], int.Parse(tmp[5]));
        }
        public static bool isSameKey(UserSchema userSchema, UserSchema userSchema2)
        {
            return userSchema.key == userSchema2.key;
        }
        public static bool isSameID(UserSchema userSchema, UserSchema userSchema2)
        {
            return userSchema.id == userSchema2.id;
        }
    }
    public class UserProcessor
    {
        private static UserProcessor SingleObject = new UserProcessor();
        private StreamReader streamReader;
        private UserProcessor()
        {

        }
        public static UserProcessor GetProcessor()
        {
            return SingleObject;
        }
        public UserSchema Get(ulong id)
        {
            UserSchema userSchema = new UserSchema();
            userSchema.id = id;
            this.streamReader = new StreamReader(System.Windows.Forms.Application.StartupPath + "\\UserList.txt");
            UserSchema tmp;
            while (!this.streamReader.EndOfStream )
            {
                try
                {
                    tmp = UserSchema.ReadStringLine(this.streamReader.ReadLine(), '^');
                }
                catch (Exception e)
                {
                    this.streamReader.Dispose();
                    return null;
                }

                if(UserSchema.isSameID(userSchema, tmp))
                {
                    this.streamReader.Dispose();
                    return tmp;
                }
            }
            this.streamReader.Dispose();
            return null;
        }
    }
}
