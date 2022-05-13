import React from 'react';
import { Outlet } from 'react-router-dom';

const HomeLayout: React.FC = () => {
	return (
		<div>
			<Outlet />
		</div>
	);
};

export default HomeLayout;
