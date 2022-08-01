import ReactDOM from 'react-dom';
import React, { useState } from "react";
import { Navbar, Nav, NavDropdown } from "react-bootstrap";
import Offcanvas from 'react-bootstrap/Offcanvas';
import ListGroup from 'react-bootstrap/ListGroup';
import Accordion from 'react-bootstrap/Accordion';

//上方導覽
function Navbarall() {
    const [show, setShow] = useState(false);
    const ShowChange = () => {
        if (show != true) {
            setShow(true);
        } else {
            setShow(false);
        }
    };
    return (
        <div>  <Navbar bg="light" expand="lg">
            <Navbar.Brand href="#home">Index</Navbar.Brand>
            <Navbar.Toggle aria-controls="navbarScroll"  />
            <Navbar.Collapse id="navbarScroll">
                <Nav className="me-auto my-2 my-lg-0" style={{ maxHeight: '100px' }} navbarScroll>
                    <Nav.Link href="/home/index">Start</Nav.Link>
                    <NavDropdown title="測試" id="basic-nav-dropdown" >
                        <NavDropdown.Item href="/BloodPressure/Index">Index</NavDropdown.Item>
                        <NavDropdown.Item href="/BloodPressure/Vue">Vue測試</NavDropdown.Item>
                        <NavDropdown.Item href="/BloodPressure/ReactIndex">React測試</NavDropdown.Item>
                        <NavDropdown.Item href="/BloodPressure/CallAPI">呼叫api測試</NavDropdown.Item>
                    </NavDropdown>

                    <Nav.Link href="#Goal">Goal</Nav.Link>
                    <Nav.Link href="#Medicatoin">Medicatoin</Nav.Link>
                    <Nav.Link onClick={ShowChange} >Measurement</Nav.Link>
                </Nav>
            </Navbar.Collapse>
            <Navbar.Collapse className="justify-content-end" id="navbarScroll">
                <Nav className="me-auto">
                    <NavDropdown title="David Wu" id="navbarScrollingDropdown"  >
                    <NavDropdown.Item href="/Account/Register">Register</NavDropdown.Item>
                    <NavDropdown.Item href="/Account/Login"> Login</NavDropdown.Item>
                    <NavDropdown.Item href="/Account/Logout"> Logout</NavDropdown.Item>
                    <NavDropdown.Item href="#Edit">Edit profile</NavDropdown.Item>
                    <NavDropdown.Item href="#Settings">Settings</NavDropdown.Item>
                    </NavDropdown>
                </Nav>
           
            </Navbar.Collapse>
        </Navbar>
           <NavLeft value={show} />
        </div>
    );
}
//側邊欄
function NavLeft(props) {

    var T = true;
    var F = false;
        return (
            <div>
                <Offcanvas show={props.value}  scroll={T} backdrop={F}>
                    <Offcanvas.Header >
                        <Offcanvas.Title>Offcanvas</Offcanvas.Title>
                    </Offcanvas.Header>
                    <Offcanvas.Body>
                        <Accordion flush defaultActiveKey="BP" flush>
                            <Accordion.Item eventKey="BP">
                                <Accordion.Header>BloodPressure</Accordion.Header>
                                <Accordion.Body>
                                    <ListGroup variant="flush">
                                        <ListGroup.Item action href="/BloodPressure/ReactIndex">
                                            Measurement
                                        </ListGroup.Item>
                                        <ListGroup.Item action href="#Progress">
                                            Progress
                                        </ListGroup.Item>
                                        <ListGroup.Item action href="#Analysis">
                                            Analysis
                                        </ListGroup.Item >
                                        <ListGroup.Item action href="#Data">
                                         Data
                                        </ListGroup.Item >
                                    </ListGroup>
                               </Accordion.Body>
                            </Accordion.Item>
                            <Accordion.Item eventKey="WG">
                                <Accordion.Header>Accordion Item #2</Accordion.Header>
                                <Accordion.Body>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                                </Accordion.Body>
                            </Accordion.Item>
                        </Accordion>
                    </Offcanvas.Body>
                </Offcanvas>
            </div>
        );
    }




const container=document.getElementById('nav');
const root= ReactDOM.createRoot(container);
root.render(<Navbarall />);