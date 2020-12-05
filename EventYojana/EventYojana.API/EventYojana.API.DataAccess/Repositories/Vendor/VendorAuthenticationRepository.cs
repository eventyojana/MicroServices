﻿using EventYojana.API.DataAccess.DataEntities.Vendor;
using EventYojana.API.DataAccess.Interfaces;
using EventYojana.API.DataAccess.Interfaces.Vendor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Repositories.Vendor
{
    public class VendorAuthenticationRepository : IVendorAuthenticationRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public VendorAuthenticationRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<RegisterVendorResponse> RegisterVendor(RegisterVendor registerVenderModel)
        {
            RegisterVendorResponse registerVendorResponse = new RegisterVendorResponse();

            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("VendorName", registerVenderModel.VendorName),
                new SqlParameter("VendorEmail", registerVenderModel.VendorEmail),
                new SqlParameter("IsBranch", false),
                new SqlParameter("Mobile", registerVenderModel.VendorMobile),
                new SqlParameter("Landline", registerVenderModel.VendorLandline),
                new SqlParameter("AddressLine", registerVenderModel.AddressLine),
                new SqlParameter("City", registerVenderModel.City),
                new SqlParameter("State", registerVenderModel.State),
                new SqlParameter("PinCode", registerVenderModel.PinCode)
            };

            SqlParameter[] outputParameter =
            {
                new SqlParameter("IsUserExists", SqlDbType.Bit),
                new SqlParameter("Success", SqlDbType.Bit),
                new SqlParameter("VendorId", SqlDbType.Int)
            };

            var result = _databaseContext.Repository<Task>().ExecuteSp(StoreProcedureSchemas.usp_RegisterVendor, sqlParameters.ToArray(), outputParameter);

            registerVendorResponse.IsUserExists = Convert.ToBoolean(result.OutParam[0].Value);
            registerVendorResponse.Success = Convert.ToBoolean(result.OutParam[1].Value);
            registerVendorResponse.VendorId = Convert.ToInt32(result.OutParam[2].Value);

            return await Task.FromResult(registerVendorResponse);
        }
    }
}
