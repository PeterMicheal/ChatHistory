import React from 'react';
import logo from './logo.svg';
import { Navbar,Nav,Form,FormControl,Button, Container } from 'react-bootstrap'
import ChatComponent from './Components/ChatComponent'
import './App.css';

function App() {
  return (
    <div>
      <div>
        <Navbar bg="dark" variant="dark">
        <Navbar.Brand >Power Diary</Navbar.Brand>
        <Nav className="mr-auto">
          <Nav.Link >Home</Nav.Link>
          <Nav.Link >Features</Nav.Link>
          <Nav.Link >Pricing</Nav.Link>
        </Nav>
        <Form inline>
          <FormControl type="text" placeholder="Search" className="mr-sm-2" />
          <Button variant="outline-info">Search</Button>
        </Form>
      </Navbar>
      <br />
      </div>
      <Container>
        <ChatComponent />
      </Container>        
    </div>
  );
}

export default App;
