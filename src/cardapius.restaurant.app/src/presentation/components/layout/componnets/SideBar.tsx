import Sider from 'antd/es/layout/Sider'
import Menu from 'antd/es/menu'
import React from 'react'
import { UploadOutlined, UserOutlined, VideoCameraOutlined, } from '@ant-design/icons';

interface SideBarProps {
    collapsed:boolean
}

export const SideBar: React.FC<SideBarProps> = (props) => {
    return (
        <Sider trigger={null} collapsible collapsed={props.collapsed}>
            <div className="demo-logo-vertical" />
            <Menu
                theme="dark"
                mode="inline"
                defaultSelectedKeys={['1']}
                items={[
                    {
                        key: '1',
                        icon: <UserOutlined />,
                        label: 'nav 1',
                    },
                    {
                        key: '2',
                        icon: <VideoCameraOutlined />,
                        label: 'nav 2',
                    },
                    {
                        key: '3',
                        icon: <UploadOutlined />,
                        label: 'nav 3',
                    },
                ]}
            />
        </Sider>
    )
}
