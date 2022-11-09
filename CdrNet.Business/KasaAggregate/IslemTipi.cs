using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business.KasaAggregate
{
	// enumlar sonradan değişemez,genişletilmez. Değişen şeyler omamalı. Haftanın günleri enum olabilir mesela
	public enum IslemTipi  // byte olmasını istersek//:byte
	{
		Gelir,
		Gider
	}
}
