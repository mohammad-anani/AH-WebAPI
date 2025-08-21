using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class OperationDoctor
    {
        public int ID { get; set; }
        public Operation Operation { get; set; }
        public Doctor Doctor { get; set; }
        public string Role { get; set; }

        public OperationDoctor()
        {
            ID = -1;
            Operation = new Operation();
            Doctor = new Doctor();
            Role = "";
        }

        public OperationDoctor(int id, Operation operation, Doctor doctor, string role)
        {
            ID = id;
            Operation = operation;
            Doctor = doctor;
            Role = role;
        }

        public OperationDoctor(Operation operation, Doctor doctor, string role)
        {
            ID = -1;
            Operation = operation;
            Doctor = doctor;
            Role = role;
        }
    }
}
