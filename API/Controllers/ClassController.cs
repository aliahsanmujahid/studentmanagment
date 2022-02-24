using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClassController: BaseApiController
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        public ClassController(DataContext context,IMapper mapper,IEmailSender emailSender)
        {
            _context = context;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [Authorize(Policy = "TeacherRole")]
        [HttpPost("create")]
        public ActionResult createClass(ClassDto classDto)
        {

        Random rnd = new Random();
        string code = rnd.Next(1000000000).ToString();     

        var newclass = new Class
        {
            AppUserId = User.GetUserId(),
            ClassName = classDto.ClassName,
            ClassCode = code
        };   

        _context.Classes.Add(newclass);
        _context.SaveChanges();


        return Ok(newclass); 
            
        }


        [Authorize(Policy = "StudentRole")]
        [HttpPost("join")]
        public ClassDto JoinClass(ClassDto classDto)
        {

        var getclass = _context.Classes.FirstOrDefault(c => c.ClassCode == classDto.ClassCode);    
        
        var joinclass = new ClassStudent
        {
           AppUserId = User.GetUserId(),
           ClassId = getclass.Id
        };

        _context.ClassStudents.Add(joinclass);
        _context.SaveChanges();

        _emailSender.SendEmailAsync(User.RetrieveEmailFromPrincipal(), "BlueBay IT",
             "You Successfully Join To Class"+getclass.ClassName);

        return _mapper.Map<Class,ClassDto>(getclass);
  
        }

        [Authorize(Policy = "TeacherRole")]
        [HttpGet("teacher")]
        public IEnumerable<ClassDto> teacherClass()
        {

         var classes = _context.Classes.Where( i => i.AppUserId == User.GetUserId());
         return _mapper.Map<IEnumerable<Class>,IEnumerable<ClassDto>>(classes);
        
        }

        [Authorize(Policy = "StudentRole")]
        [HttpGet("student")]
        public IEnumerable<ClassDto> studentClass()
        {
            
        var classes = _context.ClassStudents.AsQueryable();   
        classes = classes.Where(id =>  id.AppUserId == User.GetUserId());
        var ids = classes.Select(f => f.ClassId).ToList();
        var allclasses = _context.Classes.Where(i => ids.Contains(i.Id));

        return _mapper.Map<IEnumerable<Class>,IEnumerable<ClassDto>>(allclasses);
        
        }



               
        
    }
}