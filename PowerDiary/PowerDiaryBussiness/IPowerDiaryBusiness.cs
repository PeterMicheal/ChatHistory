﻿using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryBusiness.BusinessViewModels;

namespace PowerDiaryBusiness
{
    public interface IPowerDiaryBusiness
    {
        ServiceResponseDto<List<VrChat>> GetChatDetailedView(DateTime date);

        ServiceResponseDto<List<VrHourlyChat>> GetChatsHourView(DateTime date);
    }
}
