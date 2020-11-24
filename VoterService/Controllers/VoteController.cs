﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoterService.Models;
using VoterService.Repositories;

namespace VoterService.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        public readonly IVoteRepository _voterRepo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VoteController));

        public VoteController(IVoteRepository voterRepo)
        {
            _voterRepo = voterRepo;
        }

        // POST: Votes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Add")]
        public IActionResult Add(Vote vote)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _log4net.Info("Vote Getting Added - " + "Vote Id is " + (vote.VoteID + 1).ToString());
                    var added = _voterRepo.CastVote(vote);
                    if (added)
                    {
                        return StatusCode(201);
                    }
                    else
                    {
                        return new NoContentResult();
                    }
                }
                return StatusCode(400);
            }
            catch
            {
                _log4net.Error("Error in Adding Vote");
                return new NoContentResult();
            }
        }
    }
}