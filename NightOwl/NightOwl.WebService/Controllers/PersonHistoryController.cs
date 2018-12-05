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
        [HttpGet("{personId}")]
        public IActionResult GetByPersonId([FromRoute]int personId)
        {
            if (personId < 0)
                return BadRequest("Invalid person ID");
            try
            {
                using (var connection = new SqlConnection(Connections.sqlConnection))
                {

                    using (var selectCommand = new SqlCommand("SELECT Id, Date, SourceFaceUrl, SpottedFaceUrl, PersonId, CoordX, CoordY FROM History WHERE PersonId=@id", connection))
                    {
                        using (var dataAdapter = new SqlDataAdapter())
                        {
                            selectCommand.Parameters.AddWithValue("@id", personId);

                            dataAdapter.SelectCommand = selectCommand;

                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            DataSet dataSet = new DataSet();
                            dataAdapter.Fill(dataSet, "History");

                            return Ok(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + ex.Source);
            }
        }
    }
}