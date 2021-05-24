import React, { FC, useEffect } from 'react';
import { DropdownButton, Dropdown, Row } from 'react-bootstrap';
import Moment from 'moment';
import DatePicker from 'react-date-picker';
import DetailedView from './DetailedView';
import HourlyView from './HourlyView';
import { DetailedChatVm } from '../Types/DetailedChatVm';
import { HourlyChatVm } from '../Types/HourlyChatVm';
import { ApiService } from '../Services/ApiService';
import CSS from 'csstype';

const h1Styles: CSS.Properties = {
    paddingBottom: '17px'
};

const marigin : CSS.Properties = {
    marginRight: '10px',
    marginLeft: '10px'
  };

const labelSelectDate : CSS.Properties = {
    marginTop: '6px'
};

const ChatComponent: FC = () => {
    
    const api = new ApiService();

    const [isDetailedOrhourlyView, SetIsDetailedOrhourlyView] = React.useState<boolean>();
    const [isShowEmptyMessage, SetShowEmptyMessage] = React.useState<boolean>();
    const [DetailedViewChatData, setDetailedViewChatData] = React.useState<Array<DetailedChatVm>>();
    const [HourlyViewChatData, setHourlyViewChatData] = React.useState<Array<HourlyChatVm>>();
    var [selectedDate, setSelectedDate] = React.useState<Date>();

    useEffect(()=>{
        setSelectedDate(new Date());
        DetailedViewClick();
     },[])

     const dateSelected =async (e: any)=>{
        setSelectedDate(e);

        selectedDate = e;
        DetailedViewClick();
    }

    const DetailedViewClick =async ()=>{
        SetIsDetailedOrhourlyView(true);
        SetShowEmptyMessage(false);
        

        const formatedSelectedDate = Moment(selectedDate).format('YYYY-MM-DDTHH:mm:ss');
        api.GetListOfData<DetailedChatVm>(`chat/GetDetailedChatData/` + formatedSelectedDate) 
        .then(response => {
            var DetailedChatlist = response;
            setDetailedViewChatData( DetailedChatlist );

            if(DetailedChatlist.length === 0){
                SetShowEmptyMessage(true);
            }
        });
    }

    const HourlyViewClick =async ()=>{
        SetIsDetailedOrhourlyView(false);
        SetShowEmptyMessage(false);

        const formatedSelectedDate = Moment(selectedDate).format('YYYY-MM-DDTHH:mm:ss');
        api.GetListOfData<HourlyChatVm>(`chat/GetHourlyChatData/` + formatedSelectedDate)
        .then(response => {
            var HourlyChatlist = response;
            setHourlyViewChatData( HourlyChatlist );

            if(HourlyChatlist.length === 0){
                SetShowEmptyMessage(true);
            }
        });
    }

    return (
        <form >
            <br />
            <Row style={h1Styles} >    
                <label style={labelSelectDate}> Select Date: </label>
                <span style={marigin}>
                    <DatePicker 
                        onChange={dateSelected}
                        value={selectedDate}
                        showNavigation ={true}
                        clearIcon={null}
                        className='dropdown-basic-button height: 39px;'
                    />
                </span>
                    
                <DropdownButton 
                    id="dropdown-basic-button" title="Select View Type"
                    key="Secondary" variant="secondary" >
                    <Dropdown.Item onClick={DetailedViewClick} >Detailed View</Dropdown.Item>
                    <Dropdown.Item onClick={HourlyViewClick}  >Hourly View</Dropdown.Item>
                </DropdownButton>
            </Row>
            
            { 
                isDetailedOrhourlyView ? 
                 <DetailedView DetailedViewChatData={DetailedViewChatData} /> :
                 <HourlyView HourlyViewChatData={HourlyViewChatData} /> 
            }
            {
                isShowEmptyMessage ?
                    <span>No Data In Selected Day</span> :
                    <span></span>

            }
        </form>
    );
};

export default ChatComponent;