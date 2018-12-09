using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NightOwl.WebService.DAL;
using NightOwl.WebService.Models;

namespace NightOwl.WebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonHistoryController : ControllerBase
    {
        private int _sectionSize = 10;

        // POST: api/PersonHistory/Post/
        [HttpPost]
        public IActionResult Post([FromBody]PersonHistory historyData)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            try
            {
                using (var connection = new SqlConnection(Connections.sqlConnection))
                {
        
                    using (var insertCommand = new SqlCommand("INSERT INTO History (Date, SourceFaceURL, SpottedFaceUrl, PersonId, CoordX, CoordY) " +
                        "VALUES (@Date, @SourceFaceUrl, @SpottedFaceUrl, @PersonId, @CoordX, @CoordY)", connection))
                    {
                        insertCommand.CommandType = CommandType.Text;

                        insertCommand.Parameters.AddWithValue("@PersonId", historyData.PersonId);

                        if(historyData.Date != null)
                            insertCommand.Parameters.AddWithValue("@Date", historyData.Date);
                        else
                            insertCommand.Parameters.AddWithValue("@Date", DBNull.Value);

                        if (string.IsNullOrEmpty(historyData.SourceFaceUrl))
                            insertCommand.Parameters.AddWithValue("@SourceFaceUrl", DBNull.Value);
                        else
                            insertCommand.Parameters.AddWithValue("@SourceFaceUrl", historyData.SourceFaceUrl);

                        if (string.IsNullOrEmpty(historyData.SpottedFaceUrl))
                            insertCommand.Parameters.AddWithValue("@SpottedFaceUrl", DBNull.Value);
                        else
                            insertCommand.Parameters.AddWithValue("@SpottedFaceUrl", historyData.SpottedFaceUrl);

                        insertCommand.Parameters.AddWithValue("@CoordX", historyData.CoordX);
                        insertCommand.Parameters.AddWithValue("@CoordY", historyData.CoordY);

                        using (var dataAdapter = new SqlDataAdapter("SELECT Id, Date, SourceFaceUrl, SpottedFaceUrl, PersonId, CoordX, CoordY FROM History", Connections.sqlConnection))
                        {
                            dataAdapter.InsertCommand = insertCommand;

                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            DataRow newRow = dataTable.NewRow();
                            newRow["PersonId"] = historyData.PersonId;
                            newRow["CoordX"] = historyData.CoordX;
                            newRow["CoordY"] = historyData.CoordY;
                            newRow["SourceFaceUrl"] = historyData.SourceFaceUrl;
                            newRow["SpottedFaceUrl"] = historyData.SpottedFaceUrl;
                            newRow["Date"] = historyData.Date;

                            dataTable.Rows.Add(newRow);
                            dataAdapter.Update(dataTable);

                            return Ok(newRow);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
        }

        // GET: api/PersonHistory/GetByPersonId
        [HttpGet("{personName}/{page}")]
        public IActionResult GetByName([FromRoute]string personName, [FromRoute]int page)
        {
            if (string.IsNullOrEmpty(personName))
                return BadRequest("Invalid person Name");

            if (page < 0)
                return BadRequest("Invalid page number");

            try
            {
                using (var connection = new SqlConnection(Connections.sqlConnection))
                {
                    DataTable dtCount, historyTable;

                    using (var selectCommand = new SqlCommand("" +
                        "SELECT h.Id, h.Date, h.SourceFaceUrl, h.SpottedFaceUrl, h.PersonId, h.CoordX, h.CoordY, p.Name AS PersonName " +
                        "FROM History h, " +
                        "Persons p " +
                        "WHERE (PersonId IN (SELECT Id FROM Persons WHERE Name=@name)) " +
                        "AND p.Id = h.PersonId " +
                        "ORDER BY h.PersonId, h.Date DESC " +
                        "OFFSET @skip ROWS " +
                        "FETCH NEXT @take ROWS ONLY", connection))
                    {
                        using (var dataAdapter = new SqlDataAdapter())
                        {
                            selectCommand.Parameters.AddWithValue("@name", personName);
                            selectCommand.Parameters.AddWithValue("@skip", page * _sectionSize);
                            selectCommand.Parameters.AddWithValue("@take", _sectionSize);

                            dataAdapter.SelectCommand = selectCommand;

                            historyTable = new DataTable();
                            dataAdapter.Fill(historyTable);
                        }
                    }

                    using (var selectCommand = new SqlCommand("" +
                      "SELECT COUNT(*) " +
                      "FROM History " +
                      "WHERE PersonId IN (SELECT Id FROM Persons WHERE Name=@name) ", connection))
                    {
                        using (var dataAdapter = new SqlDataAdapter())
                        {
                            selectCommand.Parameters.AddWithValue("@name", personName);
                            dataAdapter.SelectCommand = selectCommand;

                            dtCount = new DataTable();
                            dataAdapter.Fill(dtCount);
                        }
                    }

                    int count = int.Parse(dtCount.Rows[0][0].ToString());
                    return Ok(new { PersonHistories = historyTable, PersonHistoriesCount = count });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
        }

        // GET: api/PersonHistory/Get
        [HttpGet("{page}")]
        public IActionResult Get([FromRoute]int page)
        {
            if (page < 0)
                return BadRequest("Invalid page number");

            try
            {
                using (var connection = new SqlConnection(Connections.sqlConnection))
                {
                    DataTable dtCount, historyTable;

                    using (var selectCommand = new SqlCommand("" +
                        "SELECT h.Id, h.Date, h.SourceFaceUrl, h.SpottedFaceUrl, h.PersonId, h.CoordX, h.CoordY, p.Name AS PersonName " +
                        "FROM History AS h, Persons AS p " +
                        "WHERE p.Id = h.PersonId " +
                        "ORDER BY h.Date DESC " +
                        "OFFSET @skip ROWS " +
                        "FETCH NEXT @take ROWS ONLY", connection))
                    {
                        using (var dataAdapter = new SqlDataAdapter())
                        {
                            selectCommand.Parameters.AddWithValue("@skip", page * _sectionSize);
                            selectCommand.Parameters.AddWithValue("@take", _sectionSize);

                            dataAdapter.SelectCommand = selectCommand;

                            historyTable = new DataTable();
                            dataAdapter.Fill(historyTable);

                        }
                    }

                    using (var selectCommand = new SqlCommand("SELECT COUNT(*) FROM History", connection))
                    {
                        using (var dataAdapter = new SqlDataAdapter())
                        {
                            dataAdapter.SelectCommand = selectCommand;

                            dtCount = new DataTable();
                            dataAdapter.Fill(dtCount);
                        }
                    }

                    int count = int.Parse(dtCount.Rows[0][0].ToString());
                    return Ok(new { PersonHistories = historyTable, PersonHistoriesCount = count });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
        }
    }
}