using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderCancellationController : ControllerBase
    {
        private readonly IOrderCancellation _orderCancellation;

        public OrderCancellationController(IOrderCancellation orderCancellation)
        {
            _orderCancellation = orderCancellation;
        }

        [HttpGet]
        [Route("GetOrderCancellationList")]
        public async Task<IActionResult> GetOrderCancellationList()
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.GetOrderCancellationList(bizsolESMSConnectionDetails);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrderCancellationLines")]
        public async Task<IActionResult> GetOrderCancellationLines(int Code = 0)
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.GetOrderCancellationLines(bizsolESMSConnectionDetails, Code);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrderCancellationHeader")]
        public async Task<IActionResult> GetOrderCancellationHeader(int Code)
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.GetOrderCancellationHeader(bizsolESMSConnectionDetails, Code);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrderCancellationItems")]
        public async Task<IActionResult> GetOrderCancellationItems(int Code)
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.GetOrderCancellationItems(bizsolESMSConnectionDetails, Code);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrderCancellationDetail")]
        public async Task<ActionResult<VM_OrderCancellationDetail>> GetOrderCancellationDetail(int Code)
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.GetOrderCancellationDetail(bizsolESMSConnectionDetails, Code);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrderCancellationDispatch")]
        public async Task<IActionResult> GetOrderCancellationDispatch(int Code)
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.GetOrderCancellationDispatch(bizsolESMSConnectionDetails, Code);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("SaveOrderCancellation")]
        public async Task<IActionResult> SaveOrderCancellation([FromBody] tblOrderCancellationSave model, int UserMaster_Code)
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.SaveOrderCancellation(bizsolESMSConnectionDetails, model, UserMaster_Code);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetReOpenOrderList")]
        public async Task<IActionResult> GetReOpenOrderList()
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.GetReOpenOrderList(bizsolESMSConnectionDetails);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("ReOpenOrderCancellation")]
        public async Task<IActionResult> ReOpenOrderCancellation(int Code, int UserMaster_Code)
        {
            try
            {
                var bizsolESMSConnectionDetails = CommonFunctions.InitializeERPConnection(HttpContext);
                if (bizsolESMSConnectionDetails.DefultMysqlTemp != null)
                {
                    var result = await _orderCancellation.ReOpenOrderCancellation(bizsolESMSConnectionDetails, Code, UserMaster_Code);
                    return Ok(result);
                }

                return StatusCode(500, "Error To Fetch Connection String");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
