import styled from '@mui/material/styles/styled';
import React, { PropsWithChildren } from 'react';
import Div100vh from 'react-div-100vh';

const Centered = styled('div')`
	width: 100%;
	height: 100%;
	display: flex;
	flex-direction: column;
	justify-content: center;
	align-items: center;
`;

const Center: React.FC<PropsWithChildren<{}>> = ({ children }) => {
	return (
		<Div100vh>
			<Centered>{children}</Centered>
		</Div100vh>
	);
};

export default Center;
