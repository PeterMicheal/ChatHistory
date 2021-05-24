import React, { FC } from 'react';
import Moment from 'moment';
import { Row, Col } from 'react-bootstrap';
import { HourlyChatVm } from '../Types/HourlyChatVm';

interface Props {
    HourlyViewChatData?: Array<HourlyChatVm>;
}

const HourlyView: FC<Props> = ({ HourlyViewChatData = [] }) => {
    return (
        <div>
            {HourlyViewChatData.map( (Chat, index) => (
                <Row key={index}>
                    <Col  xs={2}>{ Moment(Chat.time).format("HH:mm A") }</Col>
                    <Col xs={6}> 
                    { Chat.chatEvents.map( (chatEvent, index) => (
                        <div key={index} >{ chatEvent }</div>
                    ))}
                    </Col>
                </Row>
            ))}
        </div>
    )
}

export default HourlyView;