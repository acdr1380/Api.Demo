/*
 Navicat Premium Data Transfer

 Source Server         : 本机
 Source Server Type    : MySQL
 Source Server Version : 80026
 Source Host           : localhost:3306
 Source Schema         : test

 Target Server Type    : MySQL
 Target Server Version : 80026
 File Encoding         : 65001

 Date: 18/10/2024 17:35:50
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for sys_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_log`;
CREATE TABLE `sys_log`  (
  `Id` int UNSIGNED NOT NULL AUTO_INCREMENT,
  `Application` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Logged` datetime NULL DEFAULT NULL,
  `Level` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Message` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Logger` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Callsite` varchar(512) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Exception` varchar(512) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 63 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_log
-- ----------------------------
INSERT INTO `sys_log` VALUES (18, 'MyNlog', '2024-09-20 10:58:05', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (19, 'MyNlog', '2024-09-20 10:58:25', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (20, 'MyNlog', '2024-09-20 10:58:25', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (21, 'MyNlog', '2024-09-20 10:58:29', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (22, 'MyNlog', '2024-09-20 10:59:54', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (23, 'MyNlog', '2024-09-20 11:00:01', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (24, 'MyNlog', '2024-09-20 11:00:44', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (25, 'MyNlog', '2024-09-20 11:01:05', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (26, 'MyNlog', '2024-09-20 11:02:53', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (27, 'MyNlog', '2024-09-20 11:03:44', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (28, 'MyNlog', '2024-09-20 11:04:45', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (29, 'MyNlog', '2024-09-20 11:04:51', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (30, 'MyNlog', '2024-09-20 11:04:51', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (31, 'MyNlog', '2024-09-20 11:04:52', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (32, 'MyNlog', '2024-09-20 11:04:55', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (33, 'MyNlog', '2024-09-20 11:08:12', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (34, 'MyNlog', '2024-09-20 11:08:16', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (35, 'MyNlog', '2024-09-20 11:08:17', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (36, 'MyNlog', '2024-09-20 11:08:19', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (37, 'MyNlog', '2024-09-20 11:08:21', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (38, 'MyNlog', '2024-09-20 11:09:58', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (39, 'MyNlog', '2024-09-20 11:14:34', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (40, 'MyNlog', '2024-09-20 11:14:43', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (41, 'MyNlog', '2024-09-20 11:14:43', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (42, 'MyNlog', '2024-09-20 11:14:44', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (43, 'MyNlog', '2024-09-20 11:14:45', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (44, 'MyNlog', '2024-09-20 11:14:45', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (45, 'MyNlog', '2024-09-20 11:18:48', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (46, 'MyNlog', '2024-09-20 11:18:58', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (47, 'MyNlog', '2024-09-20 11:20:11', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (48, 'MyNlog', '2024-09-20 11:23:19', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (49, 'MyNlog', '2024-09-20 11:23:23', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (50, 'MyNlog', '2024-09-20 11:23:27', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (51, 'MyNlog', '2024-09-20 11:24:03', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (52, 'MyNlog', '2024-09-20 11:27:35', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (53, 'MyNlog', '2024-09-20 11:27:53', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (54, 'MyNlog', '2024-09-20 11:31:21', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (55, 'MyNlog', '2024-09-20 15:45:18', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (56, 'MyNlog', '2024-09-20 16:15:28', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (57, 'MyNlog', '2024-09-20 16:15:35', 'Error', '用户名或密码不正确！', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (58, 'MyNlog', '2024-09-23 10:24:54', 'Error', '服务错误：未查询到信息', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (59, 'MyNlog', '2024-09-23 10:25:01', 'Error', '服务错误：未查询到信息', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (60, 'MyNlog', '2024-09-23 10:27:15', 'Error', '服务错误：未查询到信息', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (61, 'MyNlog', '2024-09-23 10:27:22', 'Error', '服务错误：未查询到信息', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (62, 'MyNlog', '2024-09-23 10:53:26', 'Error', '服务错误：未查询到信息', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (63, 'MyNlog', '2024-10-08 11:31:35', 'Error', 'Table \'test.sys_user\' doesn\'t exist', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (64, 'MyNlog', '2024-10-18 15:41:59', 'Error', '中文提示 : Id绑定到SysUser失败,可以试着换一个类型，或者使用ORM自定义类型实现\r\nEnglish Message : SysUser Id bind error', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (65, 'MyNlog', '2024-10-18 15:43:32', 'Error', '中文提示 : Id绑定到SysUser失败,可以试着换一个类型，或者使用ORM自定义类型实现\r\nEnglish Message : SysUser Id bind error', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (66, 'MyNlog', '2024-10-18 15:45:26', 'Error', '中文提示 : Id绑定到SysUser失败,可以试着换一个类型，或者使用ORM自定义类型实现\r\nEnglish Message : SysUser Id bind error', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (67, 'MyNlog', '2024-10-18 15:46:25', 'Error', '中文提示 : Id绑定到SysUser失败,可以试着换一个类型，或者使用ORM自定义类型实现\r\nEnglish Message : SysUser Id bind error', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (68, 'MyNlog', '2024-10-18 15:48:40', 'Error', '中文提示 : Id绑定到SysUser失败,可以试着换一个类型，或者使用ORM自定义类型实现\r\nEnglish Message : SysUser Id bind error', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (69, 'MyNlog', '2024-10-18 15:49:40', 'Error', 'Table \'test.object\' doesn\'t exist', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (70, 'MyNlog', '2024-10-18 15:51:35', 'Error', '中文提示 : Id绑定到SysUser失败,可以试着换一个类型，或者使用ORM自定义类型实现\r\nEnglish Message : SysUser Id bind error', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (71, 'MyNlog', '2024-10-18 15:53:55', 'Error', '中文提示 : Id绑定到SysUser失败,可以试着换一个类型，或者使用ORM自定义类型实现\r\nEnglish Message : SysUser Id bind error', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');
INSERT INTO `sys_log` VALUES (72, 'MyNlog', '2024-10-18 15:59:01', 'Error', '中文提示 : Id绑定到SysUser失败,可以试着换一个类型，或者使用ORM自定义类型实现\r\nEnglish Message : SysUser Id bind error', 'Api.Demo.Middleware.ExceptionMiddleware', 'Api.Demo.Middleware.ExceptionMiddleware.InvokeAsync(E:\\学习\\Api.Demo\\Api.Common\\Middleware\\ExceptionMiddleware.cs:30)', '');

-- ----------------------------
-- Table structure for sys_menus
-- ----------------------------
DROP TABLE IF EXISTS `sys_menus`;
CREATE TABLE `sys_menus`  (
  `Id` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键，UUID格式',
  `Name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '菜单名称',
  `Url` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单链接',
  `ParentId` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '外键，指向自身（用于支持层级结构）',
  `CreatedTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `UpdatedTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `ParentId`(`ParentId` ASC) USING BTREE,
  CONSTRAINT `sys_menus_ibfk_1` FOREIGN KEY (`ParentId`) REFERENCES `sys_menus` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '菜单表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_menus
-- ----------------------------

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `Id` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键，UUID格式',
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户名',
  `UserAccount` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '账号',
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '邮箱（唯一）',
  `OrgId` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '外键，指向机构表',
  `CreatedTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `UpdatedTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `Username`(`UserName` ASC) USING BTREE,
  UNIQUE INDEX `Email`(`Email` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('admin', '超级管理员', 'admin', '123456', 'acdr1380@gmail.com', NULL, '2024-10-18 15:38:37', '2024-10-18 15:38:44');

SET FOREIGN_KEY_CHECKS = 1;
