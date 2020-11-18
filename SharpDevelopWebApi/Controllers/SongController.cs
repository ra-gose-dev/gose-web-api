using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	
	public class SongController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAll(string search = "", string artist = "", int? year = null, int? peak = null)
		{		
			List<Song> songs;
			if(string.IsNullOrWhiteSpace(search))
			{
				songs = _db.Songs.ToList();
				
			}
			else
			{
				songs = _db.Songs.Where(x => x.Title.ToLower().Contains(search.ToLower())
				                        || x.Artist.ToLower().Contains(search.ToLower())).OrderBy(o => o.Title).ToList();
			}
			
			if(!string.IsNullOrWhiteSpace(artist))
			{
				songs = songs.Where(x => x.Artist.ToLower() == artist.ToLower()).ToList();
			}
			
			if(year != null)
			{
				songs = songs.Where(x => x.ReleaseYear == year.Value).ToList();
			}
			
			if(peak != null)
			{
				songs = songs.Where(x => x.PeakChartPosition <= peak).ToList();
			}
			
			
			int totalCount = songs.Count();
			
			return Ok(new {totalCount, songs });
		}
				
			
		
		
		[HttpGet] 
		public IHttpActionResult Get(int Id)
		{
			var song = _db.Songs.Find(Id);
			if(song != null)
				return Ok(song);
			else
				return BadRequest("Song not found");
			
		}
		
		
	
        
		[HttpPost]
		public IHttpActionResult Create([FromBody]Song song)
		{			
			_db.Songs.Add(song);
			_db.SaveChanges();
			return Ok(song.Id);
		}
		
		[HttpPut]
		public IHttpActionResult Update([FromBody]Song updatedSong)
		{
			var song = _db.Songs.Find(updatedSong.Id);
			if(song != null)
			{
				song.Artist = updatedSong.Artist;
				song.Title = updatedSong.Title;
				song.Genre = updatedSong.Genre;
				_db.Entry(song).State = EntityState.Modified;
				_db.SaveChanges();
				
				return Ok(song);			
			}
			else
			{
				return BadRequest("Song not found");
			}

		}
		
	
		[HttpDelete]
		public IHttpActionResult Delete(int Id)
		{
			var songToDelete = _db.Songs.Find(Id);
			if(songToDelete != null)
			{
				_db.Songs.Remove(songToDelete);
				_db.SaveChanges();
				return Ok("Successfully deleted");
			}
			else
			{
				return BadRequest("Song not found");
			}
		}
	}
}