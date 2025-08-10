import LayoutAntd from 'antd/es/layout'
import { Content } from 'antd/es/layout/layout'
import theme from 'antd/es/theme';
import React, { useState } from 'react'
import { SideBar } from './componnets/SideBar';
import { Header } from './componnets/Header';

export const Layout = () => {
    const [collapsed, setCollapsed] = useState(false);
    const { token: { colorBgContainer, borderRadiusLG }, } = theme.useToken();

    return (
        <LayoutAntd>
            <SideBar collapsed={collapsed} />

            <LayoutAntd>
                <Header collapsed={collapsed} setCollapsed={setCollapsed} />
                <Content
                    style={{
                        margin: '24px 16px',
                        padding: 24,
                        minHeight: 280,
                        background: colorBgContainer,
                        borderRadius: borderRadiusLG,
                    }}
                >
                    Content
                </Content>
            </LayoutAntd>

        </LayoutAntd>
    )
}
