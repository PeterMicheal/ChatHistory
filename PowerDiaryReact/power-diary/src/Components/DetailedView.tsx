import React, { FC } from 'react';
import Moment from 'moment';
import { Row, Col } from 'react-bootstrap';
import { DetailedChatVm } from '../Types/DetailedChatVm';

interface Props {
    DetailedViewChatData?: Array<DetailedChatVm>;
}

const DetailedView: FC<Props> = ({ DetailedViewChatData = [] }) => {

    return (
        <div>
            {DetailedViewChatData.map( (Chat, index) => (
                <Row key={index}>
                    <Col> 
                        <span key={Chat.id}>{ Moment(Chat.time).format("HH:mm A")   } : { Chat.message }</span>
                    </Col>
                </Row>
                
            ))}
        </div>
    );
};

export default DetailedView;