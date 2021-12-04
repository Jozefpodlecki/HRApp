import background from "./../assets/background.jpg";
import styled from "styled-components";

const Background = styled.div`
    position: absolute;
    width: 100%;
    height: 100%;
    background-image: url(${background});
    z-index: -1;
`;

export default Background;
