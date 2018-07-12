using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElectronicLicenceServer.Models
{
    public partial class ElectronicLicContext : DbContext
    {
        public ElectronicLicContext()
        {
        }

        public ElectronicLicContext(DbContextOptions<ElectronicLicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Jsz> Jsz { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Xsz> Xsz { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.CardId);

                entity.Property(e => e.CardId).ValueGeneratedNever();

                entity.Property(e => e.Cllx).IsRequired();

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Hphm).IsRequired();

                entity.Property(e => e.Sfzmhm).IsRequired();

                entity.Property(e => e.Type).IsRequired();
            });

            modelBuilder.Entity<Jsz>(entity =>
            {
                entity.HasKey(e => e.IdNum);

                entity.ForNpgsqlHasComment("驾驶证");

                entity.Property(e => e.IdNum)
                    .HasColumnName("idNum")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdNum);

                entity.ForNpgsqlHasComment("用户信息 : id  身份证号 电话 姓名 token 行驶证  驾驶证");

                entity.Property(e => e.IdNum).ValueGeneratedNever();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Phone).IsRequired();

                entity.Property(e => e.Token).IsRequired();
            });

            modelBuilder.Entity<Xsz>(entity =>
            {
                entity.HasKey(e => new {e.Hphm, e.Cllx});

//                entity.ForNpgsqlHasComment("行驶证信息: 
//                hphm: \"\",  // 号牌号码
//                cllx: \"\", // 车辆类型
//                syr: \"\", // 所有人
//                zzxxdz: \"\", // 地址
//                syxz: \"\", // 使用性质
//                clpp1: \"\", // 品牌
//                clxh: \"\", // c车辆型号
//                clsbdh: \"\", // 车辆识别代号
//                fdjh: \"\", // 发动机号
//                fzrq: \"\", // 发证日期
//                dabh: \"\",
//                ccdjrq: \"\", // 注册日期
//                hdzk: \"\", // 核定载客
//                zzl: \"\", // 总质量
//                zbzl: \"\",
//                cwkc: \"\", // 长
//                cwkk: \"\", // 宽
//                cwkg: \"\", // 高
//                rlzl: \"\", // 燃料种类
//                yxqz: \"\", // 有效期止
//                gxrq: \"\",
//                xszbh: \"\", // 一维码内容");

                entity.Property(e => e.Hphm).HasColumnName("hphm");

                entity.Property(e => e.Cllx).HasColumnName("cllx");

                entity.Property(e => e.Ccdjrq).HasColumnName("ccdjrq");

                entity.Property(e => e.Clpp1).HasColumnName("clpp1");

                entity.Property(e => e.Clsbdh).HasColumnName("clsbdh");

                entity.Property(e => e.Clxh).HasColumnName("clxh");

                entity.Property(e => e.Cwkc).HasColumnName("cwkc");

                entity.Property(e => e.Cwkg).HasColumnName("cwkg");

                entity.Property(e => e.Cwkk).HasColumnName("cwkk");

                entity.Property(e => e.Dabh).HasColumnName("dabh");

                entity.Property(e => e.Fdjh).HasColumnName("fdjh");

                entity.Property(e => e.Fzrq).HasColumnName("fzrq");

                entity.Property(e => e.Gxrq).HasColumnName("gxrq");

                entity.Property(e => e.Hdzk).HasColumnName("hdzk");

                entity.Property(e => e.IdNum)
                    .IsRequired()
                    .HasColumnName("idNum");

                entity.Property(e => e.Rlzl).HasColumnName("rlzl");

                entity.Property(e => e.Syr).HasColumnName("syr");

                entity.Property(e => e.Syxz).HasColumnName("syxz");

                entity.Property(e => e.Xszbh).HasColumnName("xszbh");

                entity.Property(e => e.Yxqz).HasColumnName("yxqz");

                entity.Property(e => e.Zbzl).HasColumnName("zbzl");

                entity.Property(e => e.Zzl).HasColumnName("zzl");

                entity.Property(e => e.Zzxxdz).HasColumnName("zzxxdz");
            });
        }
    }
}