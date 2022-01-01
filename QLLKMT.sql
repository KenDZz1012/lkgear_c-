create database QLLKMT_C#

use QLLKMT_C#
go

create table NhanVien(
	MaNV int IDENTITY(100,1) primary key,
	TenNV nvarchar(50),
	Avatar image,
	fileAnh nvarchar(50),
	TenChucVu nvarchar(20),
	GioiTinh nvarchar(20),
	NgSinh date,
	CMND nvarchar(20),
	SDT nvarchar(20),
	Luong nvarchar(20),
	SoNgayLam int,
	TaiKhoan nvarchar(20),
	MatKhau nvarchar(20),
)

drop table SanPham
create table ChucVu(
	TenChucVu nvarchar(20) primary key,
	MaChucVu int IDENTITY(200,1),
	SLNV int,
)

create table LoaiSP(
	TenLSP nvarchar(20) primary key,
	MaLSP int IDENTITY(300,1),
	SLSP int,
)

create table NhaCC(
	TenNhaCC nvarchar(20) primary key,
	MaNhaCC int IDENTITY(400,1) ,
	SLSP int,
)

create table SanPham(
	MaSP int IDENTITY(500,1) primary key,
	AnhSP image,
	fileAnh nvarchar(50),
	TenSP nvarchar(50),
	TenLSP nvarchar(20),
	TenNhaCC nvarchar(20),
	SoLuong int,
	SoLuongHong int,
	DonGia nvarchar(20),
	GiaNhap nvarchar(20),
	BaoHanh nvarchar(20),
)

create table HoaDon(
	MaHD int IDENTITY(600,1) primary key,
	MaNV int ,
	MaKH int,
	NgayHD date,
	TongTien nvarchar(50),
	TongTienNhap nvarchar(50),
	TrangThai nvarchar(20),
)
drop table HoaDon


create table CTHoaDon(
	MaHD int not null,
	MaSP int not null,
	Qty int,
	primary key(MaHD,MaSP),
)

create table KhachHang(
	MaKH int IDENTITY(700,1) primary key,
	TenKH nvarchar(50),
	SDT varchar(20),
	Email nvarchar(50),
	GiaTriMua nvarchar(50),
	LoaiKH nvarchar(20),
)

create table BaoHanh(
	MaBH int IDENTITY(800,1) primary key,
	MaSP int,
	TenNhaCC nvarchar(20),
	Qty int,
	NgayBH date,
	TinhTrang nvarchar(20)
)

create table TuyenDung(
	MaTD int IDENTITY(900,1) primary key,
	TenTD nvarchar(20),
	TenChucVu nvarchar(20),
	GioiTinh nvarchar(20),
	NgSinh date,
	CMND nvarchar(20),
	SDT nvarchar(20),
	GioiThieu nvarchar(1000),
	Avatar image,
	fileAnh nvarchar(50),
)
drop table TuyenDung

drop table HoaDon
UPDATE NhanVien 
SET SoNgayLam = 120						
Where MaNV like '%NV%'

Create Trigger TG_TongTienNhap_HoaDon_insert on CTHoaDon
for insert, update
as
	Update HoaDon
	Set TongTienNhap = (select sum(SanPham.GiaNhap*CTHoaDon.Qty)
					from CTHoaDon, SanPham, HoaDon
					where CTHoaDon.MaSP = SanPham.MaSP and HoaDon.MaHD = CTHoaDon.MaHD and HoaDon.MaHD = (select MaHD from inserted) 
					group by HoaDon.MaHD)
	Where HoaDon.MaHD = (select MaHD from inserted)


Create Trigger TG_TongTienNhap_HoaDon_delete on CTHoaDon
for delete
as
	Update HoaDon
	Set TongTienNhap = (select sum(SanPham.GiaNhap*CTHoaDon.Qty)
					from CTHoaDon, SanPham, HoaDon
					where CTHoaDon.MaSP = SanPham.MaSP and HoaDon.MaHD = CTHoaDon.MaHD and HoaDon.MaHD = (select MaHD from deleted) 
					group by HoaDon.MaHD)
	Where HoaDon.MaHD = (select MaHD from deleted)








Create Trigger TG_TongTien_HoaDon_insert on CTHoaDon
for insert, update
as
	Update HoaDon
	Set TongTien = (select sum(SanPham.DonGia*CTHoaDon.Qty)
					from CTHoaDon, SanPham, HoaDon
					where CTHoaDon.MaSP = SanPham.MaSP and HoaDon.MaHD = CTHoaDon.MaHD and HoaDon.MaHD = (select MaHD from inserted) 
					group by HoaDon.MaHD)
	Where HoaDon.MaHD = (select MaHD from inserted)



Create Trigger TG_TongTien_HoaDon_delete on CTHoaDon
for delete
as
	Update HoaDon
	Set TongTien = (select sum(SanPham.DonGia*CTHoaDon.Qty)
					from CTHoaDon, SanPham, HoaDon
					where CTHoaDon.MaSP = SanPham.MaSP and HoaDon.MaHD = CTHoaDon.MaHD and HoaDon.MaHD = (select MaHD from deleted) 
					group by HoaDon.MaHD)
	Where HoaDon.MaHD = (select MaHD from deleted)




create trigger updateSL on NhanVien
for insert,update
as
update ChucVu
Set SLNV = (Select Count(*)
			from NhanVien,ChucVu
			Where NhanVien.TenChucVu = ChucVu.TenChucVu and ChucVu.TenChucVu = (select TenChucVu from inserted)
			group by ChucVu.TenChucVu,NhanVien.TenChucVu)
where ChucVu.TenChucVu = (select TenChucVu from inserted)

drop trigger updateSL

create trigger xoaSLNV on NhanVien
for delete,update
as
update ChucVu
Set SLNV = (Select Count(*)
			from NhanVien,ChucVu
			Where NhanVien.TenChucVu = ChucVu.TenChucVu and ChucVu.TenChucVu = (select TenChucVu from deleted)
			group by ChucVu.TenChucVu,NhanVien.TenChucVu)
where ChucVu.TenChucVu = (select TenChucVu from deleted)





create trigger updateSLSP on SanPham
for insert,update
as
update NhaCC
Set SLSP = (Select Count(*)
			from SanPham,NhaCC
			Where SanPham.TenNhaCC = NhaCC.TenNhaCC and NhaCC.TenNhaCC = (select TenNhaCC from inserted)
			group by NhaCC.TenNhaCC)
where NhaCC.TenNhaCC = (select TenNhaCC from inserted)

create trigger xoaSLSP on SanPham
for delete,update
as
update NhaCC
Set SLSP = (Select Count(*)
			from SanPham,NhaCC
			Where SanPham.TenNhaCC = NhaCC.TenNhaCC and NhaCC.TenNhaCC = (select TenNhaCC from deleted)
			group by NhaCC.TenNhaCC)
where NhaCC.TenNhaCC = (select TenNhaCC from deleted)

drop trigger xoaSLSP
update ChucVu set SLNV = 0 where TenChucVu = 'Quản Lý'

create trigger updateSLSP_LSP on SanPham
for insert,update
as
update LoaiSP
Set SLSP = (Select Count(*)
			from SanPham,LoaiSP
			Where SanPham.TenLSP = LoaiSP.TenLSP and LoaiSP.TenLSP = (select TenLSP from inserted)
			group by LoaiSP.TenLSP)
where  LoaiSP.TenLSP = (select TenLSP from inserted)


create trigger xoaSLSP_LSP on SanPham
for delete,update
as
update LoaiSP
Set SLSP = (Select Count(*)
			from SanPham,LoaiSP
			Where SanPham.TenLSP = LoaiSP.TenLSP and LoaiSP.TenLSP = (select TenLSP from deleted)
			group by LoaiSP.TenLSP)
where  LoaiSP.TenLSP = (select TenLSP from deleted)










	

Update NhanVien
Set SoNgayLam = 0

Insert into HoaDon(MaHD,MaNV,NgayHD,TongTien,TrangThai)values('HD01','NV01','12/10/2001','0','no')
Update HoaDon set MaKH = 'KH01' where MaHD = 'HD01'

INSERT INTO KhachHang(MaKH)  VALUES ('KH01')
ON DUPLICATE KEY UPDATE MaKH='KH01'

Select SanPham.MaSP, SanPham.TenSP, SanPham.DonGia, CTHoaDon.Qty, Sum(CTHoaDon.Qty*SanPham.DonGia) as 'ThanhTien' 
from HoaDon, CTHoaDon, SanPham 
where HoaDon.MaHD = CTHoaDon.MaHD and SanPham.MaSP = CTHoaDon.MaSP and HoaDon.MaHD = 'HD07'
group by HoaDon.MaHD, SanPham.MaSP, SanPham.TenSP, SanPham.DonGia, CTHoaDon.Qty

