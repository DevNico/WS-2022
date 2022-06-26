import React, { PropsWithChildren } from 'react';
import Div100vh from 'react-div-100vh';
import Center from './Center';

const FullScreenCenter: React.FC<PropsWithChildren<{}>> = ({ children }) => {
	return (
		<Div100vh>
			<Center>{children}</Center>
		</Div100vh>
	);
};

export default FullScreenCenter;
