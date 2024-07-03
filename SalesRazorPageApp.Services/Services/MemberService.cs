using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Shared.Exceptions;

namespace SalesRazorPageApp.Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateMember(Member member)
        {

            await _unitOfWork.MemberRepository.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();

            return member.MemberId;
        }

        public async Task DeleteMember(int memberId)
        {
            if (!await _unitOfWork.MemberRepository.AnyAsync(m => m.MemberId == memberId))
            {
                throw new NotFoundException("Member not found");
            }

            await _unitOfWork.MemberRepository.ExecuteDeleteAsync(m => m.MemberId == memberId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Member> GetMemberDetailsById(int id)
        {
            var member = await _unitOfWork.MemberRepository.GetMemberDetailsById(id);

            if (member is null)
            {
                throw new NotFoundException("Member not found");
            }

            return member;
        }

        public async Task<List<Member>> GetMembers()
        {
            return await _unitOfWork.MemberRepository.GetAllAsync();
        }

        public async Task UpdateMember(Member member)
        {
            await _unitOfWork.MemberRepository.UpdateAsync(member);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}