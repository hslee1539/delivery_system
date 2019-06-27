using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 택배_시스템
{
    /// <summary>
    /// 바코드
    /// </summary>
    public class NoData : Exception
    {

    }
    public interface IBarcodeAdapter
    {
        /// <summary>
        /// 바코드를 읽어 옵니다.
        /// </summary>
        /// <tip>이 메소드는 타이머나 스래드에 의해 신호를 받게끔 만들자!</tip>
        /// <returns>바코드가 인식이 되는 경우, 부호 없는 64비트 정수를 반환합니다.</returns>
        /// <exception cref="NoData">인식이 되지 않는 경우 발생하게 만들어야 함.</exception>
        ulong Get();
    }
    class TestBarcodeAdapter : IBarcodeAdapter
    {
        // 모의 바코드 인식 대기 카운터
        private int count = 0;
        public ulong Get()
        {
            this.count++;
            if(this.count > 100)
            {
                return 201401985;
            }
            else
            {
                throw new NoData();
            }
        }
    }
}
