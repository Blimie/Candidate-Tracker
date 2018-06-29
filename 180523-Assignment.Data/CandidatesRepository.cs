using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _180523_Assignment.Data
{
    public enum Status
    {
        Pending,
        Confirmed,
        Refused
    }

    public class CandidatesRepository
    {
        private string _connectionString;

        public CandidatesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddCandidate(Candidate candidate)
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                context.Candidates.InsertOnSubmit(candidate);
                context.SubmitChanges();
            }
        }
        public void SetStatus(int id, Status status)
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                context.ExecuteCommand("UPDATE Candidates SET Status = {1}  WHERE Id = {0}", id, status);
            }
        }    
        public IEnumerable<Candidate> GetCandidates()
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {     
                return context.Candidates.ToList();
            }
        }
        public IEnumerable<Candidate> GetCandidatesByStatus(Status status)
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                return context.Candidates.Where(c => c.Status == status).ToList();
            }
        }           
        public Candidate GetById(int id)
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                return context.Candidates.FirstOrDefault(i => i.Id == id);
            }
        }    
        public int GetCandidateCountByStatus(Status status)
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                return context.Candidates.Count(c => c.Status == status);
            }
        }      
    }
}
