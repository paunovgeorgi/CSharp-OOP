using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {

        private SubjectRepository subjects = new SubjectRepository();
        private StudentRepository students = new StudentRepository();
        private UniversityRepository universities = new UniversityRepository();
        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(EconomicalSubject) && subjectType != nameof(HumanitySubject) && subjectType != nameof(TechnicalSubject))
            {
                return String.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            if (subjects.Models.Any(s=>s.Name == subjectName))
            {
                return String.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            ISubject subject = null;

            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjects.Models.Count + 1, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(subjects.Models.Count + 1, subjectName);
            }
            else
            {
                subject = new TechnicalSubject(subjects.Models.Count + 1, subjectName);
            }

            subjects.AddModel(subject);

            return String.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName,
                nameof(SubjectRepository));
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.Models.Any(u=>u.Name == universityName))
            {
                return String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            //List<int> ids = new List<int>();

            //foreach (ISubject subject in subjects.Models)
            //{
            //    foreach (string requiredSubject in requiredSubjects)
            //    {
            //        if (requiredSubject == subject.Name)
            //        {
            //            ids.Add(subject.Id);
            //        }
            //    }
            //}


            List<int> ids = new List<int>();
            foreach (var subName in requiredSubjects)
            {
                ids.Add(this.subjects.FindByName(subName).Id);
            }

            IUniversity university = new University(universities.Models.Count + 1, universityName, category, capacity, ids);
            universities.AddModel(university);

            return String.Format(OutputMessages.UniversityAddedSuccessfully, universityName,
                nameof(UniversityRepository));
        }

        public string AddStudent(string firstName, string lastName)
        {
            if (students.Models.Any(s=>s.FirstName == firstName && s.LastName == lastName))
            {
                return String.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            IStudent student = new Student(students.Models.Count + 1,firstName, lastName);
            students.AddModel(student);

            return String.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName,
                nameof(StudentRepository));
        }

        public string TakeExam(int studentId, int subjectId)
        {
            if (!students.Models.Any(s=>s.Id == studentId))
            {
                return String.Format(OutputMessages.InvalidStudentId);
            }

            if (!subjects.Models.Any(s=>s.Id == subjectId))
            {
                return String.Format(OutputMessages.InvalidSubjectId);
            }

            IStudent student = students.Models.FirstOrDefault(s => s.Id == studentId);
            ISubject subject = subjects.Models.FirstOrDefault(s => s.Id == subjectId);

            if (student.CoveredExams.Contains(subjectId))
            {
                return String.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName,
                    subject.Name);
            }

            student.CoverExam(subject);
            return String.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName,
                subject.Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] fullName = studentName.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string firstName = fullName[0];
            string lastName = fullName[1];

            if (!students.Models.Any(s=>s.FirstName == firstName && s.LastName == lastName))
            {
                return String.Format(OutputMessages.StudentNotRegitered, firstName, lastName);
            }

            if (!universities.Models.Any(u=>u.Name == universityName))
            {
                return String.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            IStudent student = students.Models.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
            IUniversity university = universities.Models.FirstOrDefault(u => u.Name == universityName);

            foreach (int universityRequiredSubject in university.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(universityRequiredSubject))
                {
                    return String.Format(OutputMessages.StudentHasToCoverExams, studentName,universityName);
                }
            }

            if (student.University == university)
            {
                return String.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName);
            }

            student.JoinUniversity(university);

            return String.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);

        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.Models.FirstOrDefault(u => u.Id == universityId);

            //int studentsCount = students.Models.Select(s => s.University == university).Count();

            int studentsCount = 0;

            foreach (IStudent student in students.Models)
            {
                if (student.University == university)
                {
                    studentsCount++;
                }
            }

            StringBuilder sb = new();
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentsCount}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentsCount}");

            return sb.ToString().TrimEnd();
        }
    }
}
