import React from 'react';
import { Route, Routes } from 'react-router-dom';
import HomeLayout from './layouts/HomeLayout';
import HomePage from './pages/HomePage';
import NotFoundPage from './pages/NotFoundPage';
import OrganisationsPage from './pages/OrganisationsPage';
import CreateUsersPage from './pages/CreateUsersPage';
import UnauthorizedPage from './pages/UnauthorizedPage';
import CreateRolePage from './pages/CreateRolePage';

const Router: React.FC = () => {
	return (
		<Routes>
			<Route path='/' element={<HomeLayout />}>
				<Route index element={<HomePage />} />
				<Route path='/organisations' element={<OrganisationsPage />} />
				<Route path='/users/create' element={<CreateUsersPage />} />
				<Route path='/roles/create' element={<CreateRolePage />} />
				<Route path='/unauthorized' element={<UnauthorizedPage />} />
			</Route>
			<Route path='*' element={<NotFoundPage />} />
		</Routes>
	);
};

export default Router;
